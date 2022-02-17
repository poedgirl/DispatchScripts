<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>LINQPad.Controls</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

void Main()
{
	"Channels".Dump();
	var submitBtn = new Button("Submit");
	var result = new TextArea();
	
	List<Channel> channels = new List<Channel>();

	var clickHandler = () =>
	{
		var sb = new StringBuilder();
		sb.AppendLine("311 ");
		sb.AppendLine("📡 Radio status:");
		foreach (var channel in channels.Where(c => !string.IsNullOrWhiteSpace(c.Input.Text)))
		{
			sb.Append($"📢 CH{channel.ChannelNo}: {channel.Input.Text}");
			if (channel.Units.Text != "0") {
				sb.AppendLine($" ({channel.Units.Text} unit{(channel.Units.Text != "1" ? "s" : "")})");
			} else {
				sb.AppendLine();
			}
		}
		
		result.Text = sb.ToString();
		result.Focus();
		result.SelectAll();
		
		Clipboard.SetText(result.Text.Trim());
	};


	channels = Enumerable.Range(1, 10).Select(idx => {
		var input = new TextBox();
		var units = new TextBox();
		units.HtmlElement.SetAttribute("type", "number");
		units.Width = "50px";
		units.Text = "0";

		var div = new Div(new Span($"Channel {idx}: "), input, units);
		
		EventHandler<PropertyEventArgs> keyDownEvent = (object sender, PropertyEventArgs args) => {
			if (args.Properties["code"] == "Enter" || args.Properties["code"] == "NumpadEnter") {
				clickHandler();
			}
		};

		input.HtmlElement.AddEventListener("keydown", new[] { "code" }, keyDownEvent);
		units.HtmlElement.AddEventListener("keydown", new[] { "code" }, keyDownEvent);
		
		return new Channel {
			ChannelNo = idx,
			Container = div,
			Units = units,
			Input = input
		};
	}).ToList();
	
	channels[1].Input.Text = "Normal Patrol";
	
	foreach (var channel in channels)
	{
		channel.Container.Dump();
	}
	
	submitBtn.HtmlElement.SetAttribute("type", "submit");
	
	var seventyEightButton = new Button("🚨 78 🚨");
	seventyEightButton.Click += (sender, args) =>
	{
		Clipboard.SetText("311 \n🚨❗10-78 requested❗🚨 CH");
	};

	var thirteenButton = new Button("🚨 13A 🚨");
	thirteenButton.Click += (sender, args) =>
	{
		Clipboard.SetText("311 \n🚨❗10-13A❗🚨 CH");
	};

	new Div(submitBtn, seventyEightButton, thirteenButton).Dump();
	
	
	result.Dump();
	
	submitBtn.Click += (sender, args) =>
	{
		clickHandler();
	};
	
	clickHandler();
}

// You can define other methods, fields, classes and namespaces here
public class Channel
{
	public int ChannelNo { get; set; }
	public Div Container { get; set; }
	public TextBox Input { get; set; }
	public TextBox Units { get; set; }
}