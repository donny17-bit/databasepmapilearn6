using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMMenu
{
    public class Dropdown : VMDropdown
    {
        // constructor
        private Dropdown(string value, string text) : base(value, text)
        {
        }

        public static Dropdown[] FromDb(MMenu[] menu) =>
            menu.Select(m => new Dropdown(m.ID.ToString(), m.Name))
            .ToArray();
    }
}