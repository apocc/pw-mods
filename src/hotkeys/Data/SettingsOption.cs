namespace Apocc.Pw.Hotkeys.Data
{
    public enum SettingsOptionType { Text, CheckBox }

    public sealed class SettingsOption
    {
        public string Description { get; private set; }
        public string Id { get; private set; }
        public string Label { get; private set; }
        public string Property { get; private set; }
        public SettingsOptionType Type { get; private set; }

        public SettingsOption(string label, string property, SettingsOptionType type, string id = null, string description = null)
        {
            Label = label;
            Property = property;
            Type = type;
            Id = id;
            Description = description;
        }
    }
}
