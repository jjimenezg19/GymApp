﻿using System.Data.SqlClient;
using DataAccess.Dao;
using DTO;


namespace DataAccess.Mapper
{
    public class UserMapper : ICrudStatements, IObjectMapper

    {
        //Metodo que construye una lista objetos tipo usuario 
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            //NO ESTA IMPLEMENTADO
            List<BaseClass> UserList = new List<BaseClass>();

            return UserList;
        }

        //Metodo que construye un objeto tipo usuario 
        public BaseClass BuildObject(Dictionary<string, object> result)
        {
            var user = new User();

            if (result.ContainsKey("user_id") && int.TryParse(result["user_id"].ToString(), out int userId))
                user.Id = userId;

            if (result.ContainsKey("role_id") && int.TryParse(result["role_id"].ToString(), out int roleId))
                user.Role_id = roleId;

            user.First_name = result.ContainsKey("first_name") ? result["first_name"].ToString() : null;
            user.Last_name = result.ContainsKey("last_name") ? result["last_name"].ToString() : null;
            user.Username = result.ContainsKey("username") ? result["username"].ToString() : null;
            user.Email = result.ContainsKey("email") ? result["email"].ToString() : null;
            user.Password = result.ContainsKey("password") ? result["password"].ToString() : null;
            user.Phone_number = result.ContainsKey("phone_number") ? result["phone_number"].ToString() : null;

            if (result.ContainsKey("birthdate") && DateTime.TryParse(result["birthdate"].ToString(), out DateTime birthdate))
                user.Birthdate = birthdate;
            else
                user.Birthdate = default;

            user.Profile_image = result.ContainsKey("profile_image") ? result["profile_image"].ToString() : null;

            user.Id_image = result.ContainsKey("id_image") ? result["id_image"].ToString() : null;

            return user;
        }

        //Metodo para registrar un suario 
        public SqlOperation GetRegisterUser(BaseClass entityDTO, string hashedPassword, SqlParameter newUserIdParam)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_addUserAccount";

            User user = (User)entityDTO;

            operation.AddIntegerParam("role_id", user.Role_id);
            operation.AddVarcharParam("first_name", user.First_name);
            operation.AddVarcharParam("last_name", user.Last_name);
            operation.AddVarcharParam("username", user.Username);
            operation.AddVarcharParam("email", user.Email);
            operation.AddVarcharParam("password", hashedPassword);
            operation.AddVarcharParam("phone_number", user.Phone_number);
            operation.AddDateTimeParam("birthdate", user.Birthdate);
            operation.AddVarcharParam("profile_image",user.Profile_image);
            operation.AddVarcharParam("id_image", user.Id_image);

            operation.parameters.Add(newUserIdParam);

            return operation;
        }

        //Metodo para registrar la Salt de la ocntraseña
        public SqlOperation GetRegisterSalt(int userId, string salt)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_addUserSalt";

            operation.AddIntegerParam("user_id", userId);
            operation.AddVarcharParam("salt", salt);

            return operation;
        }

        //Metodo que retorna un usuario por el email 
        public SqlOperation GetRetrieveByEmailStatement(string email)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getUserByEmail";

            operation.AddVarcharParam("Email", email);

            return operation;
        }

        //Metodo que retorna un usuario por el username 
        public SqlOperation GetRetrieveUserByUsername(string username)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getUserByUsername";

            operation.AddVarcharParam("username", username);

            return operation;
        }

        //Metodo que retorna un usuario por el id 
        public SqlOperation GetRetrieveSaltByUserId(int userId)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_getUserSaltByUserId";

            operation.AddIntegerParam("user_id", userId);

            return operation;
        }

        //Metodo para guardar la nueva contraseña
        public SqlOperation GetRegisterToken(int userId, string token)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_addUserToken";

            operation.AddIntegerParam("user_id", userId);
            operation.AddVarcharParam("token", token);

            return operation;
        }

        //Metodo para uctualizar la nueva contraseña en la tabla usuario
        public SqlOperation GetUpdatePasswordByToken(string token, string hasedPassword, string salt)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_updateUserPassword";

            operation.AddVarcharParam("token", token);
            operation.AddVarcharParam("password", hasedPassword);
            operation.AddVarcharParam("salt", salt);

            return operation;
        }

        //Metodo que retorna un usuario por el Id
        public SqlOperation GetRetrieveByIdStatement(int Id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "";

            operation.AddIntegerParam("Id", Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseClass entityDTO)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }
    }
}