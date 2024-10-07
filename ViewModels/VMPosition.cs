using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMPosition
{
    public class Dropdown : VMDropdown
    {
        private Dropdown(string value, string text) : base(value, text)
        { }

        public static Dropdown[] FromDb(MPosition[] position)
        {
            return position.Select(m => new Dropdown(m.Id.ToString(), m.Name)).ToArray();
        }
    }
}