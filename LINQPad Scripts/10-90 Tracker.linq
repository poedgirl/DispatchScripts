<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>LINQPad.Controls</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

void Main()
{
	var institutionNames = new string[] {
		"Bay City Bank",
		"Paleto Bank",
		"Jewelry Store",
		"Fleeca GOHWY",
		"Fleeca Harmony",
		"Bobcat",
		"Vault",
		"Yacht",
		"Casino"
	};
	
	new LINQPad.Controls.Literal("h1", "10-90 Tracker").Dump();
	"Tick off the institutions that have been hit or are currently being hit".Dump();
	"As soon as you change one of them, it'll copy the text below to the clipboard".Dump();

	var submitBtn = new Button("Submit");
	var result = new TextArea();

	List<Institution> institutions = institutionNames.Select(n => new Institution
	{
		Name = n,
		Status = InstStatus.NotHit,
		Channel = 2,
		BuiltIn = true
	}).ToList();

	institutions.AddRange(Enumerable.Range(1, 5).Select(e => new Institution
	{
		Status = InstStatus.NotHit,
		Channel = 2
	}).ToList());
	
	var table = new Table(true);
	table.HeaderStyles["text-align"] = "center";
	table.HeaderStyles["vertical-align"] = "bottom";
	var headerControls = new List<Control>();
	var statuses = typeof(InstStatus).GetFields().Where(f => f.IsLiteral)
		.ToDictionary(i => i.GetValue(null), i => i.GetCustomAttribute<InstStatusAttribute>());
		
	headerControls.AddRange(statuses.Select(s => new StackPanel(false, new Span(s.Value.Emoji), new Span(s.Value.Name))));
	headerControls.Add(new Span("Name"));
	headerControls.Add(new Span("Channel"));
	
	var header = new TableRow(true, headerControls);
	table.Rows.Add(header);

	var updateStatus = () =>
	{
		var sb = new StringBuilder();
		sb.AppendLine("311 ");
		sb.AppendLine("Shift-3's 10-90 o'Clock Tracker");

		foreach (var i in institutions.Where(i => !string.IsNullOrEmpty(i.Name)))
		{
			var channel = i.Status == InstStatus.HasResponse && i.Channel != 0 ? $" (CH{i.Channel})" : "";
			sb.AppendLine($"{statuses[i.Status].Emoji}  {i.Name}{channel}");
		}

		result.Text = sb.ToString();
		Clipboard.SetText(result.Text.Trim());
	};

	var setStatus = (InstStatus status, Institution inst) => {
		inst.Status = status;
		
		updateStatus();
	};
	
	foreach (var inst in institutions)
	{
		var instIdx = institutions.IndexOf(inst);
		var row = new TableRow();
		foreach (var s in statuses)
		{
			var radioButton = new RadioButton(
				instIdx.ToString(), 
				"", 
				(InstStatus)s.Key == InstStatus.NotHit, 
				(sender) => setStatus((InstStatus)s.Key, inst)
			);
			
			radioButton.Styles["margin-right"] = "0";
			var cell = new TableCell(radioButton);
			cell.Styles["text-align"] = "center";
			row.Cells.Add(cell);
		}
		
		if (inst.BuiltIn) {
			row.Cells.Add(new TableCell(new Span(inst.Name)));
		} else {
			row.Cells.Add(new TableCell(new TextBox(inst.Name, "10em", (sender) => inst.Name = sender.Text)));
		}
		
		var channelBox = new TextBox(inst.Channel.ToString(), "3em", (sender) => inst.Channel = int.Parse(sender.Text));
		channelBox.HtmlElement.SetAttribute("type", "number");
		row.Cells.Add(new TableCell(channelBox));
		
		table.Rows.Add(row);
	}
	
	table.Dump();
	
	submitBtn.Click += (sender, args) => updateStatus();

	new Div(submitBtn).Dump();
	result.Dump();
	updateStatus();
}

// You can define other methods, fields, classes and namespaces here
public enum InstStatus : int
{
	[InstStatus("❌", "Not Hit")]
	NotHit = 1,
	[InstStatus("🚨", "Being Hit")]
	BeingHit,
	[InstStatus("🚓", "Has Response")]
	HasResponse,
	[InstStatus("✔️", "Been Hit")]
	Hit
}

public class InstStatusAttribute : Attribute
{
	public InstStatusAttribute(string emoji, string name) {
		Emoji = emoji;
		Name = name;
	}
	public string Emoji {get;set;}
	public string Name {get;set;}
}

public class Institution
{
	public InstStatus Status {get;set;}
	public string Name {get;set;}
	public int Channel {get;set;}
	public bool BuiltIn {get;set;}
}