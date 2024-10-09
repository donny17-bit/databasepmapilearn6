using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMFileType
{
    public class Dropdown : VMDropdown
    {
        public Dropdown(string value, string text) : base(value, text)
        {
        }

        public static Dropdown[] FromDb(MFileType[] FileTypes)
        {
            return FileTypes.Select(m => new Dropdown(m.Id.ToString(), m.name)).ToArray();
        }

    }
}