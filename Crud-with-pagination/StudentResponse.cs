using Crud_with_pagination.Models;

namespace Crud_with_pagination
{
    public class StudentResponse
    {
        public List<TblStudent> Students { get; set; } = new List<TblStudent>();

        public int Pages { get; set; }

        public int CurrentPages { get; set; }

        public int TotalRecords { get; set; }

        

        
    }
}
