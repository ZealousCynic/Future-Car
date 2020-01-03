using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StatisticRepository : SQLRepository<Statistic>
    {
        public override void Create(Statistic entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Statistic entity)
        {
            throw new NotImplementedException();
        }

        public override Statistic[] GetAll()
        {
            throw new NotImplementedException();
        }

        public override Statistic GetById(int id)
        {
            CmdText = "GetLatestReadings";

            Cmd.Connection = Connection;
            Cmd.CommandText = CmdText;

            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter[] sqlParams = new[]
            {
            new SqlParameter("@VortexId", id),
            new SqlParameter("@Temperature", System.Data.SqlDbType.Int),
            new SqlParameter("@Compas", System.Data.SqlDbType.Int),
            new SqlParameter("@WaterLevel", System.Data.SqlDbType.Int),
            new SqlParameter("@Powerstate", System.Data.SqlDbType.Bit),
            new SqlParameter("@Drivestate", System.Data.SqlDbType.Bit),
            };

            //Skip param 1 in loop - not output.
            for (int i = 1; i < sqlParams.Length; i++)
                sqlParams[i].Direction = System.Data.ParameterDirection.Output;

            Cmd.Parameters.AddRange(sqlParams);

            Cmd.Connection.Open();
            Cmd.ExecuteNonQuery();

            return new Statistic
            {
                Temperature = (int)sqlParams[1].Value,
                Compas = (int)sqlParams[2].Value,
                WaterLevel = (int)sqlParams[3].Value,
                Powerstate = (bool)sqlParams[4].Value,
                Drivestate = (bool)sqlParams[5].Value
            };
        }

        public override void Update(Statistic entity)
        {
            throw new NotImplementedException();
        }
    }
}
