using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMFileCategory
{
    public class Dropdown : VMDropdown
    {
        public Dropdown(string value, string text) : base(value, text)
        {
        }

        public static Dropdown[] FromDb(MFileCategory[] fileCategories)
        {
            return fileCategories.Select(m => new Dropdown(m.Id.ToString(), m.Name)).ToArray();
        }
    }
}