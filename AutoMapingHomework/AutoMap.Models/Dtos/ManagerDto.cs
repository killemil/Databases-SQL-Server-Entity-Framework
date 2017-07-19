namespace AutoMap.Models.Dtos
{
    using System.Collections.Generic;
    using System.Text;

    public class ManagerDto
    {
        public ManagerDto()
        {
            this.Subordinates = new HashSet<EmployeeDto>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<EmployeeDto> Subordinates { get; set; }

        public int SubordinatesCount { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.FirstName} {this.LastName} | Employees: {this.SubordinatesCount}");

            foreach (EmployeeDto subordinate in this.Subordinates)
            {
                sb.AppendLine(subordinate.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
