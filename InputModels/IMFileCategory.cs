namespace databasepmapilearn6.InputModels;

public class IMFileCategory
{
    public class Dropdown : IMDropdown
    {
        public int[] file_types { get; set; }
    }
}