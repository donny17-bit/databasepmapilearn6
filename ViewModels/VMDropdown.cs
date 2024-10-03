namespace databasepmapilearn6.ViewModels;

public abstract class VMDropdown
{
    // The value when a select list option is selected.
    public string value { get; set; }

    // Text/label to be displayed for the corresponding value.
    public string text { get; set; }

    // constructor
    protected VMDropdown(string value, string text) { this.value = value; this.text = text; }
}