using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMUnit
{

    public class Dropdown : VMDropdown
    {
        // constructor
        private Dropdown(string value, string text) : base(value, text)
        { }

        public static Dropdown[] FromDb(MUnit[] units)
        {
            return units.Select(m => new Dropdown(
                m.Id.ToString(), m.Name
            )).ToArray();
        }
    }
}