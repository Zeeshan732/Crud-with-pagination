using Crud_with_pagination.Models;

public class IPageStudent
{
    public List<TblStudent> PageStudents { get; set; } = new List<TblStudent>();
    public int StudentsTotalCount { get; set; }
}
