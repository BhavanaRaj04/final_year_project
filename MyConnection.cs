using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace CloudBasedEncryptedCrime
{
    public class MyConnection
    {
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataAdapter adp = null;

        public MyConnection()
        {
            con = new MySqlConnection("server=localhost;database=cloudbasecrime;user id=root;password=root;port=3307;");
            con.Open();
        }
        public int LoginVerify(string UserId, string Password, string UserType)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = "";
            if (UserType == "Application Manager")
            {
                sql = string.Format("Select count(*) from applicationmanager where AMId='{0}' and Password='{1}'", UserId, Password);
            }
            else if (UserType == "Police Station")
            {
                sql = string.Format("Select count(*) from policestationmaster where PoliceStationId={0} and Password='{1}'", UserId, Password);
            }
            else if (UserType == "FSL Staff")
            {
                sql = string.Format("Select count(*) from fslstaff where FSLId={0} and Password='{1}'", UserId, Password);
            }
            else if (UserType == "Police Staff")
            {
                sql = string.Format("Select count(*) from policestaff where PoliceStaffId={0} and Password='{1}'", UserId, Password);
            }
            cmd.CommandText = sql;
            int result = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return result;
        }
        
        public DataTable GetEmailStaff(string UserId, string UserType)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = "";
            if (UserType == "Police Station")
            {
                sql = string.Format("Select * from policestationmaster where PoliceStationId={0}", UserId);
            }
            else if (UserType == "FSL Staff")
            {
                sql = string.Format("Select * from fslstaff where FSLId={0}", UserId);
            }
            else if (UserType == "Police Staff")
            {
                sql = string.Format("Select * from policestaff where PoliceStaffId={0}", UserId);
            }
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string ChangePassword(string UserId, string Password, string UserType)
        {

            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = "";
            if (UserType == "Application Manager")
            {
                sql = string.Format("Update applicationmanager set Password='{0}' where AMId='{1}'", Password, UserId);
            }
            else if (UserType == "Police Station")
            {
                sql = string.Format("Update policestationmaster set Password='{0}' where PoliceStationId={1}", Password, UserId);
            }
            else if (UserType == "FSL Staff")
            {
                sql = string.Format("Update fslstaff set Password='{0}' where FSLId={1}", Password, UserId);
            }
            else if (UserType == "Police Staff")
            {
                sql = string.Format("Update policestaff set Password='{0}' where PoliceStaffId={1}", Password, UserId);
            }
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }
        public string CreateArea(string AreaName)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlchk = string.Format("Select count(*) from areamaster where AreaName='{0}'", AreaName);
            cmd.CommandText = sqlchk;
            int cnt = int.Parse(cmd.ExecuteScalar().ToString());
            string result = "";
            if (cnt == 0)
            {
                string sql = string.Format("insert into areamaster(AreaName)values('{0}')", AreaName);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();
            }
            else
            {
                result = "2";
            }
            con.Close();
            return result;
        }
        public DataTable GetArea()
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from areamaster");
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string AddPoliceStaff(int PoliceStaffId, string Name, string Password,string StaffRole, string MobileNo, string EmailId, string Address)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string chksql = string.Format("Select count(*) from policestaff where EmailId='{0}'", EmailId);
            cmd.CommandText = chksql;
            string res = cmd.ExecuteScalar().ToString();
            string result = "";
            if (res == "0")
            {
                string sql = string.Format("insert into policestaff(PoliceStaffId,Name,Password,StaffRole,MobileNo,EmailId,Address)values({0},'{1}','{2}','{3}','{4}','{5}','{6}')", PoliceStaffId, Name, Password,StaffRole, MobileNo, EmailId, Address);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();
            }
            else
            {
                result = "2";
            }
            con.Close();
            return result;
        }
        public string CreatePoliceStation(int PoliceStationId, int AreaId, string Name, string Password, string MobileNo, string EmailId, string Pincode, string Address)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string chksql = string.Format("Select count(*) from policestationmaster where AreaId={0}", AreaId);
            cmd.CommandText = chksql;
            string res = cmd.ExecuteScalar().ToString();
            string result = "";
            if (res == "0")
            {
                string sql = string.Format("insert into policestationmaster(PoliceStationId,AreaId,Name,Password,MobileNo,EmailId,Pincode,Address)values({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}')", PoliceStationId, AreaId, Name, Password, MobileNo, EmailId, Pincode, Address);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();
            }
            else
            {
                result = "2";
            }
            con.Close();
            return result;
        }
        public string CreateFSLStaff(int FSLId, string Name, string Password, string MobileNo, string EmailId, string Address)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string chksql = string.Format("Select count(*) from fslstaff where EmailId='{0}'", EmailId);
            cmd.CommandText = chksql;
            string res = cmd.ExecuteScalar().ToString();
            string result = "";
            if (res == "0")
            {
                string sql = string.Format("insert into fslstaff(FSLId,Name,Password,MobileNo,EmailId,Address)values({0},'{1}','{2}','{3}','{4}','{5}')", FSLId, Name, Password, MobileNo, EmailId, Address);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();
            }
            else
            {
                result = "2";
            }
            con.Close();
            return result;
        }
        public DataTable GetPoliceStation()
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from policestationmaster");
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public DataTable GetPoliceStaff_Role()
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select concat(CAST(PoliceStaffId AS char(25)),'-',Name,'-',StaffRole) as Name,PoliceStaffId from policestaff");
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string MapPoliceStaff_PS(int PoliceStaffId, int PoliceStationId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string chksql = string.Format("Select count(*) from mappolicestaff where PoliceStaffId={0} and PoliceStationId={1}", PoliceStaffId, PoliceStationId);
            cmd.CommandText = chksql;
            string res = cmd.ExecuteScalar().ToString();
            string result = "";
            if (res == "0")
            {
                string sql = string.Format("insert into mappolicestaff(PoliceStaffId,PoliceStationId)values({0},{1})", PoliceStaffId, PoliceStationId);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();
            }
            else
            {
                result = "2";
            }
            con.Close();
            return result;
        }
        public string AddCrime(int PSId, string CrimeName, string CrimePlace, string Description)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = string.Format("insert into crimemaster(PSId,CrimeName,CrimePlace,Description,CrimeDate,Status)values({0},'{1}','{2}','{3}','{4}','Pending')", PSId, CrimeName, CrimePlace, Description, DateTime.Now);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }
        public DataTable GetPoliceStaff_Role(int PSId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select concat(CAST(policestaff.PoliceStaffId AS char(25)),'-',policestaff.Name,'-',policestaff.StaffRole) as Name,policestaff.PoliceStaffId from policestaff inner join mappolicestaff on mappolicestaff.PoliceStaffId=policestaff.PoliceStaffId where mappolicestaff.PoliceStationId={0}", PSId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }

        public DataTable GetPoliceStaff_Info(int PSId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("Select * from policestaff where PoliceStaffId={0}", PSId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }

        public DataTable GetCrime(int PSId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("Select * from crimemaster where PSId={0} and (Status='Pending' or Status='Process')", PSId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string MapPoliceStaffCrime(int CrimeId, int PSId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string chksql = string.Format("Select count(*) from policestaffcrimemap where PSId={0} and CrimeId={1}", PSId, CrimeId);
            cmd.CommandText = chksql;
            string res = cmd.ExecuteScalar().ToString();
            if (res == "0")
            {
                string sql = string.Format("insert into policestaffcrimemap(PSId,CrimeId,LogDate,Status)values({0},{1},'{2}','Active')", PSId, CrimeId, DateTime.Now.ToString());
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();
            }
            else
            {
                result = "2";
            }
            con.Close();
            return result;
        }
        public string AddForensicDC(int FSId, int CrimeId, string Description)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = string.Format("insert into forensicdatacollect(FSId,CrimeId,Description,FSCDate)values({0},{1},'{2}','{3}')", FSId, CrimeId, Description, DateTime.Now);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }
        public DataTable GetForensicStaff(int FSId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("Select * from fslstaff where FSLId={0}", FSId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string CreateTable_FSRG(int PoliceStationId, int CrimeId, int FSId, string LogDate, string FKey1, string FKey2, string FilePath)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sqlcu = string.Format("update crimemaster set Status='Process' where CrimeId={0}", CrimeId);
            cmd.CommandText = sqlcu;
            result = cmd.ExecuteNonQuery().ToString();
            if (result == "1")
            {
                string sql = string.Format("insert into forensicreport(PSId,CId,FSId,LogDate,FKey1,FKey2,FilePath)values({0},{1},{2},'{3}','{4}','{5}','{6}')", PoliceStationId, CrimeId, FSId, LogDate, FKey1, FKey2, FilePath);
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery().ToString();
            }
            con.Close();
            return result;
        }
        public DataTable GetCrime_PoliceStaff(int PoliceStaffId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("Select crimemaster.CrimeId,crimemaster.CrimeName from crimemaster inner join policestaffcrimemap on policestaffcrimemap.CrimeId=crimemaster.CrimeId where policestaffcrimemap.PSId={0}", PoliceStaffId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public DataTable GetFSReport(int CrimeId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("Select * from forensicreport where CId={0}", CrimeId);
            cmd.CommandText = sql;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public DataTable GetFSReportData(int FSRId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sqlfsd = string.Format("Select * from forensicreport where FSRId={0}", FSRId);
            cmd.CommandText = sqlfsd;
            adp = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            adp.Fill(tab);
            con.Close();
            return tab;

        }
        public string CrimeInvestigationLog(int CrimeId, string FilePath, string FKey1, string FKey2)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = "";
            sql = string.Format("insert into crimelog(CrimeId,LogDate,FilePath,FKey1,FKey2)values({0},'{1}','{2}','{3}','{4}')", CrimeId, DateTime.Now.ToString(), FilePath, FKey1, FKey2);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }
        public DataTable GetCrimeInvestigationLog(int CrimeId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from crimelog where CrimeId={0}", CrimeId);
            cmd.CommandText = sql;
            DataTable tab = new DataTable();
            adp = new MySqlDataAdapter(cmd);
            adp.Fill(tab);
            con.Close();
            return tab;
        }
        public string UpdateCrimeInvestigationLog(int CrimeId, string FilePath, string FKey1, string FKey2)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = "";
            sql = string.Format("update crimelog set FilePath='{0}',FKey1='{1}',FKey2='{2}' where CrimeId={3}", FilePath, FKey1, FKey2, CrimeId);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }

        public string AddEvidenceLog(int CrimeId,int StaffId, string AWSFilePath, string DataKeyPath)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string result = "";
            string sql = "";
            sql = string.Format("insert into evidencelog(CrimeId,StaffId,LogDate,AWSFilePath,DataKeyPath)values({0},{1},'{2}','{3}','{4}')", CrimeId, StaffId, DateTime.Now.ToString(), AWSFilePath, DataKeyPath);
            cmd.CommandText = sql;
            result = cmd.ExecuteNonQuery().ToString();
            con.Close();
            return result;
        }

        public DataTable GetCrimeEvidenceLog(int CrimeId)
        {
            cmd = new MySqlCommand();
            cmd.Connection = con;
            string sql = string.Format("select * from evidencelog where CrimeId={0}", CrimeId);
            cmd.CommandText = sql;
            DataTable tab = new DataTable();
            adp = new MySqlDataAdapter(cmd);
            adp.Fill(tab);
            con.Close();
            return tab;
        }
    }
}