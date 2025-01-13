using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_with_pagination.Models;
using Crud_with_pagination.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace StudentManagement.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRecordContext _context;

        public StudentController(StudentRecordContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<TblStudent>>> GetStudent()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<TblStudent>>> GetStudentByPage(int pageIndex, int PageSize, string? sortBy, string? sortOrder, string? filterValue)
        {
            var query = _context.Students.Select(s => s);

            if (!string.IsNullOrEmpty(filterValue))
            {
                query = query.Where(s => s.Name.ToLower().Contains(filterValue.ToLower()) ||
                         s.Email.ToLower().Contains(filterValue.ToLower()));

            }

            switch (sortBy)
            {
                case "Name":
                    query = sortOrder == "desc" ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name);
                    break;

                case "Email":
                    query = sortOrder == "desc" ? query.OrderByDescending(s=> s.Email) : query.OrderBy(s => s.Email);
                    break;

                case "Department":
                    query = sortOrder == "desc" ? query.OrderByDescending(s => s.Department) : query.OrderBy(s => s.Department);
                    break;
                default:
                    query = query.OrderBy(s => s.Id);
                    break;
                        
            }

            //var onePageStudent = query.Skip(PageSize*pageIndex).Take(PageSize);

            var onePageStudent = query.Skip(PageSize * (pageIndex - 1)).Take(PageSize);


            var totalCount = await query.CountAsync();

            var pageResult = await onePageStudent.ToListAsync();
            return Ok(new
            {
                PageStudents = pageResult,
                StudentsTotalCount = totalCount
            });


        }

            [HttpGet("{id}")]
        public async Task<ActionResult<TblStudent>> GetStudentById(int id)
        {
            var tblStudent = await _context.Students.FindAsync(id);

            if (tblStudent == null)
            {
                return NotFound();
            }

            return tblStudent;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] TblStudent tblStudent)
        {
            if (id != tblStudent.Id)
            {
                return BadRequest("ID is not Valid");
            }

            var validationResult = new StudentValidator();
            var result = validationResult.Validate(tblStudent);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            _context.Entry(tblStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblStudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tblStudent);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] TblStudent student)
        {
            var validator = new StudentValidator();
            var result = validator.Validate(student);
            
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            
            var existingStudent = _context.Students.FirstOrDefault(s => s.Email == student.Email);
            if (existingStudent != null)
            {
                return BadRequest("This email is already registered!");
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return CreatedAtAction(nameof(CreateStudent), new { id = student.Id }, student);
        }


        [HttpGet("check-email/{email}")]
        public IActionResult CheckEmailExists(string email)
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == email);
            if (student != null)
            {
                return Ok(true); // Email exists
            }
            return Ok(false); // Email does not exist
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var tblStudent = await _context.Students.FindAsync(id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            _context.Students.Remove(tblStudent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblStudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
