using APBDExam.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBDExam.Services
{
    public class DbService : IDbService
    {

        private const string ConString = "Data Source=DESKTOP-ENIT2G5\\" +
            "SQLEXPRESS;Initial Catalog=clinicDb;Integrated Security=True";

        public int deletePatientFromDb(int IdPatient)
        {

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var trans = con.BeginTransaction();
                com.Transaction = trans;


                try
                {
                    //czy pacjent istnieje w bd?
                    com.CommandText = "SELECT IdPatient From Patients WHERE IdPaient = @IdPatient";
                    com.Parameters.AddWithValue("IdPatient", IdPatient);
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        trans.Rollback();
                        throw new ClinicException("Dany pacjent nie istnieje w bazie danych");
                    }
                    dr.Close();
                    // usun recepty z bazy dla pacjenta o IdPatient (jeśli są)
                    com.CommandText = "DELETE FROM Prescription WHERE IdPatient = @IdPatient ";
                    com.Parameters.AddWithValue("IdPatient", IdPatient);
                    com.ExecuteNonQuery();


                    //USUŃ PACJENTA
                    com.CommandText = "DELETE FROM Patients WHERE Idpatient = @IdPatient";
                    com.Parameters.AddWithValue("IdPatient", IdPatient);
                    return com.ExecuteNonQuery();


                }
                catch (SqlException e)
                {
                    trans.Rollback();
                    return 0;
                }


            }
        }

        public MedicamentPrescriptionsResponse getMedicamentData(int IdMedicament)
        {
           
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                //pobierz dane z tabeli IdMedicament połączonej z Prescription_medicament połączonej z Prescription dla Medicament o id Idmedicament
                com.CommandText = "SELECT m.IdMedicament , m.Name, m.Description, m.Type , pm.IdPrescription, p.Date " +
                    "FROM Medicaments m JOIN Prescription_Medicaments pm on m.IdMedicament = pm.IdMedicament " +
                    "Join Prescriptions p on pm.IdPrescription = p.PatientIdPatient WHERE  m.IdMedicament = @ IdMedicament";

                com.Parameters.AddWithValue("IdMedicament", IdMedicament);

                var dr = com.ExecuteReader();
                if (dr.Read()) throw new ClinicException("Nie ma danego Id leku w bazie");

                //przypisz odczytane dane do obiektu  MedicamentPrescriptionsResponse

                var response = new MedicamentPrescriptionsResponse
                {
                    IdMedicament = int.Parse(dr["IdMedicament"].ToString()),
                    Name = dr["Name"].ToString(),
                    Description = dr["Description"].ToString(),
                    Type = dr["Type"].ToString(),
                  //  Prescriptions = dr["IdPrescription"].ToString();
                    Date = DateTime.Parse(dr["Date"].ToString())

                };



                return response;
            }
            
        }
    }
}
