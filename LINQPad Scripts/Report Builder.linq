<Query Kind="Statements">
  <Output>DataGrids</Output>
  <Namespace>LINQPad.Controls</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

const string ReportTakenBy = "D-28 K. Stokes";

"Report Title".Dump();
var reportTitleInput = new TextBox();
new Div(reportTitleInput).Dump();

"Reporter's Name".Dump();
var reporterNameInput = new TextBox();
new Div(reporterNameInput).Dump();

"Reporter's ID".Dump();
var reporterIdInput = new TextBox();
new Div(reporterIdInput).Dump();

"Reporter's Phone Number".Dump();
var reporterPhoneInput = new TextBox();
new Div(reporterPhoneInput).Dump();

"Incident Date".Dump();
var incidentDateInput = new TextBox();
new Div(incidentDateInput).Dump();

"Incident Time".Dump();
var incidentTimeInput = new TextBox();

var incidentTimeEST = new CheckBox("Is EST?");
new Div(incidentTimeInput, incidentTimeEST).Dump();

"Incident Location".Dump();
var incidentLocationInput = new TextBox();
new Div(incidentLocationInput).Dump();

"Witnesses".Dump();
var witnessInput = new TextArea();
new Div(witnessInput).Dump();

"Vehicles".Dump();
var vehicleInput = new TextArea();
new Div(vehicleInput).Dump();

"Suspect Description".Dump();
var suspectDescriptionInput = new TextArea();
new Div(suspectDescriptionInput).Dump();

var statementInput = new TextArea();
statementInput.Rows = 30;
var statementDiv = new Div(new Span("Statement"), statementInput);
statementDiv.Styles["display"] = "flex";
statementDiv.Styles["flex-direction"] = "column";

var statementNotesInput = new TextArea();
statementNotesInput.Rows = 30;
var statementNotesDiv = new Div(new Span("Statement Notes"), statementNotesInput);
statementNotesDiv.Styles["display"] = "flex";
statementNotesDiv.Styles["flex-direction"] = "column";

var notesDiv = new Div(statementDiv, statementNotesDiv);
notesDiv.Styles["display"] = "flex";
notesDiv.Dump();

var submitBtn = new Button("Submit");
submitBtn.HtmlElement.SetAttribute("type", "submit");
new Div(submitBtn).Dump();

var div = new Div().Dump();
div.Styles["white-space"] = "pre-wrap";

var incDateTime = DateTime.Now;
incidentDateInput.Text = incDateTime.ToString("yyyy-MM-dd");
incidentTimeInput.Text = incDateTime.ToString("HH:mm");

var reportDateTime = DateTime.UtcNow.AddHours(-5);

var result = new TextArea();
result.Rows = 40;
result.Dump();

submitBtn.Click += (sender, args) =>
{
	var reportBuilder = new StringBuilder();
	
	var reportDate = reportDateTime.ToString("dd/MM/yy");
	var reportTime = reportDateTime.ToString("HH:mm") + " EST";
	
	var incidentDateTime = DateTime.Parse(incidentDateInput.Text + " " + incidentTimeInput);
	if (!incidentTimeEST.Checked) {
		incidentDateTime = incidentDateTime.ToUniversalTime().AddHours(-5);
	}
	var incidentDate = incidentDateTime.ToString("dd/MM/yy");
	var incidentTime = incidentDateTime.ToString("HH:mm") + " EST";

	reportBuilder.AppendLine($"{reporterNameInput.Text} | {reportTitleInput.Text} | {reportDate}");
	reportBuilder.AppendLine();
	reportBuilder.AppendLine("----");
	reportBuilder.AppendLine("Dispatch Report:");
	reportBuilder.AppendLine($"Report taken by: {ReportTakenBy}");
	reportBuilder.AppendLine($"Date of Report: {reportDate} {reportTime}");
	reportBuilder.AppendLine();
	reportBuilder.AppendLine($"Date of Incident: {incidentDate} {incidentTime}");
	reportBuilder.AppendLine();
	reportBuilder.AppendLine("Reporting Person:");
	reportBuilder.AppendLine($"Name: {reporterNameInput.Text}");
	reportBuilder.AppendLine($"SID: {reporterIdInput.Text}");
	reportBuilder.AppendLine($"Ph#: {reporterPhoneInput.Text}");
	reportBuilder.AppendLine();

	if (!string.IsNullOrEmpty(witnessInput.Text))
	{
		var hasOneWitness = witnessInput.Text.Split("\n", StringSplitOptions.None).Count() == 1;
		reportBuilder.AppendLine($"Person{(hasOneWitness ? "" : "s")} Involved:");
		reportBuilder.AppendLine(witnessInput.Text);
		reportBuilder.AppendLine();
	}

	if (!string.IsNullOrEmpty(suspectDescriptionInput.Text))
	{
		var hasOneSuspect = suspectDescriptionInput.Text.Split("\n", StringSplitOptions.None).Count() == 1;
		reportBuilder.AppendLine($"Suspect Description{(hasOneSuspect ? "" : "s")}:");
		reportBuilder.AppendLine(suspectDescriptionInput.Text);
		reportBuilder.AppendLine();
	}

	if (!string.IsNullOrEmpty(vehicleInput.Text))
	{
		var hasOneVehicle = vehicleInput.Text.Split("\n", StringSplitOptions.None).Count() == 1;
		reportBuilder.AppendLine($"Vehicle Description{(hasOneVehicle ? "" : "s")}:");
		reportBuilder.AppendLine(vehicleInput.Text);
		reportBuilder.AppendLine();
	}

	if (!string.IsNullOrEmpty(incidentLocationInput.Text))
	{
		reportBuilder.AppendLine($"Location of incident:");
		reportBuilder.AppendLine(incidentLocationInput.Text);
		reportBuilder.AppendLine();
	}
	
	reportBuilder.AppendLine("Statement:");
	reportBuilder.AppendLine(statementInput.Text);

	result.Text = reportBuilder.ToString();

	result.Focus();
	result.SelectAll();

	Clipboard.SetText(result.Text.Trim());
};