using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMJobType
{
    public class Dropdown : VMDropdown
    {
        private Dropdown(string value, string text) : base(value, text)
        { }

        public static Dropdown[] FromDb(MJobType[] jobTypes)
        {
            return jobTypes.Select(m => new Dropdown(m.Id.ToString(), m.Name)).ToArray();
        }
    }
}