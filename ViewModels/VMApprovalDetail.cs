using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMApprovalDetail
{
    public int level { get; set; }
    public string posisi { get; set; }
    public List<MUser> user { get; set; }

    public class Dropdown : VMDropdown
    {
        private Dropdown(string value, string text) : base(value, text)
        { }


        // public static Dropdown[] FromDb(MApprovalDetail[] role)
        // {
        //     return role.Select(m => new Dropdown(m.Id.ToString(), m.Name)).ToArray();
        // }
    }
}