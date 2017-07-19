namespace _10Excercise
{
    using System.Data.Entity;

    public class HospitalExtendedContext : DbContext
    {

        public HospitalExtendedContext()
            : base("name=HospitalExtendedContext")
        {
        }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Diagnose> Diagnoses { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<Visitation> Visitations { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }

    }

}