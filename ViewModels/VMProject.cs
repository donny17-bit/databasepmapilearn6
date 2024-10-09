using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMProject
{
    public class Dropdown : VMDropdown
    {
        private Dropdown(string value, string text) : base(value, text)
        { }

        public static Dropdown[] FromDb(MProject[] projects)
        {
            var arr = projects.Select(m => new Dropdown(m.Id.ToString(), m.Name)).ToArray();
            return arr;
        }
    }
}