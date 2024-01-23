using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Nodes;

namespace QLHV_API.Models
{
    public class connectString
    {
        string connectionString = "Server=HoangAnh;Initial Catalog=QLHV;Persist Security Info=True;User ID=sa;Password=123456";
        public SqlConnection connect()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public DataTable convertArr(string result)
        {
           
            DataTable dataTable = new DataTable();
            if (result.ToString() == "") return dataTable;
            // Parse chuỗi JSON thành một mảng các đối tượng dynamic
            dynamic[] dataObjects = JsonConvert.DeserializeObject<dynamic[]>(result.ToString());

            // Nếu có dữ liệu trong mảng
            if (dataObjects.Length > 0)
            {
                // Lặp qua thuộc tính của đối tượng đầu tiên để tạo cột cho DataTable
                foreach (var property in dataObjects[0].Properties())
                {
                    dataTable.Columns.Add(property.Name, typeof(string));
                }

                // Lặp qua từng đối tượng để thêm dữ liệu vào DataTable
                foreach (var dataObject in dataObjects)
                {
                    DataRow row = dataTable.NewRow();

                    foreach (var property in dataObject.Properties())
                    {
                        row[property.Name] = property.Value.ToString();
                    }

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
        public Object convertObj(string result)
        {
            Object ob = new Object();
            // Parse chuỗi JSON thành một mảng các đối tượng dynamic
            dynamic jsonObject = JsonConvert.DeserializeObject(result);
          
            // Nếu có dữ liệu trong mảng
            if (result!=null)
            {
                // Lặp qua thuộc tính của đối tượng đầu tiên để tạo cột cho DataTable
               

                // Lặp qua từng đối tượng để thêm dữ liệu vào DataTable
                
                ob = jsonObject;

             
                
            }

            return ob;
        }
        public DataTable getGV(string para1,string para2,string para3)
        {
            string kq;
            SqlConnection connection = new SqlConnection(connectionString);
           

            SqlCommand command = new SqlCommand("select dbo.GetDiembyTK(@para1,@para2,@para3)", connection);

            command.CommandType = CommandType.Text; // hoặc CommandType.Text nếu bạn gọi một truy vấn SQL bình thường
                                                    // Đặt tham số và giá trị tham số (nếu có)
            command.Parameters.Add(new SqlParameter("@para1", para1));
            command.Parameters.Add(new SqlParameter("@para2", para2));
            command.Parameters.Add(new SqlParameter("@para3", para3));                              //command.Parameters.Add(new SqlParameter("@ParameterName", d));

            connection.Open();

            // ExecuteScalar() để lấy giá trị số lượng nhân viên
            var result = command.ExecuteScalar();
            kq = result.ToString();

            connection.Close();
            return convertArr(kq);
        }
        public DataTable getDiembyLop(int id)
        {
            string kq;
            SqlConnection connection = new SqlConnection(connectionString);


            SqlCommand command = new SqlCommand("select [dbo].[getDiembyLop](@id)", connection);

            command.CommandType = CommandType.Text; // hoặc CommandType.Text nếu bạn gọi một truy vấn SQL bình thường
                                                    // Đặt tham số và giá trị tham số (nếu có)
                                                    command.Parameters.Add(new SqlParameter("@id", id));
            connection.Open();

            // ExecuteScalar() để lấy giá trị số lượng nhân viên
            var result = command.ExecuteScalar();
            kq = result.ToString();

            connection.Close();
            return convertArr(kq);
        }

            public DataTable get(string fuction)
        {
            string kq;
            SqlConnection connection = new SqlConnection(connectionString);


                SqlCommand command = new SqlCommand(fuction, connection);
                
                    command.CommandType = CommandType.Text; // hoặc CommandType.Text nếu bạn gọi một truy vấn SQL bình thường
                                                            // Đặt tham số và giá trị tham số (nếu có)
                                                            //command.Parameters.Add(new SqlParameter("@ParameterName", d));
                    connection.Open();

                    // ExecuteScalar() để lấy giá trị số lượng nhân viên
                    var result = command.ExecuteScalar();
                    kq= result.ToString();

                    connection.Close();


            //return result;
            // Phân tích chuỗi JSON thành danh sách đối tượng dynamic
            DataTable dataTable = new DataTable();

            // Parse chuỗi JSON thành một mảng các đối tượng dynamic
            dynamic[] dataObjects = JsonConvert.DeserializeObject<dynamic[]>(result.ToString());

            // Nếu có dữ liệu trong mảng
            if (dataObjects.Length > 0)
            {
                // Lặp qua thuộc tính của đối tượng đầu tiên để tạo cột cho DataTable
                foreach (var property in dataObjects[0].Properties())
                {
                    dataTable.Columns.Add(property.Name, typeof(string));
                }

                // Lặp qua từng đối tượng để thêm dữ liệu vào DataTable
                foreach (var dataObject in dataObjects)
                {
                    DataRow row = dataTable.NewRow();

                    foreach (var property in dataObject.Properties())
                    {
                        row[property.Name] = property.Value.ToString();
                    }

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
            //return new JsonResult("No data found.");

        }
      
        private IActionResult Content(string jsonString, string v)
        {
            throw new NotImplementedException();
        }

        public object get(string function,string user)
        {

            string kq;
            SqlConnection connection = new SqlConnection(connectionString);


            SqlCommand command = new SqlCommand(function + "(@user)", connection);

            command.CommandType = CommandType.Text; // hoặc CommandType.Text nếu bạn gọi một truy vấn SQL bình thường
                                                    // Đặt tham số và giá trị tham số (nếu có)
            command.Parameters.Add(new SqlParameter("@user", user));
                                      //command.Parameters.Add(new SqlParameter("@ParameterName", d));

            connection.Open();

            // ExecuteScalar() để lấy giá trị số lượng nhân viên
            var result = command.ExecuteScalar();
            kq = result.ToString();

            connection.Close();
            return convertArr(kq);

        }
        public object getById(string function,string user,Guid id)
        {



            string kq;
            SqlConnection connection = new SqlConnection(connectionString);


            SqlCommand command = new SqlCommand(function + "(@user,@id)", connection);

            command.CommandType = CommandType.Text; // hoặc CommandType.Text nếu bạn gọi một truy vấn SQL bình thường
                                                    // Đặt tham số và giá trị tham số (nếu có)
            command.Parameters.Add(new SqlParameter("@user", user));

            command.Parameters.Add(new SqlParameter("@id", id));
            //command.Parameters.Add(new SqlParameter("@ParameterName", d));

            connection.Open();

            // ExecuteScalar() để lấy giá trị số lượng nhân viên
            var result = command.ExecuteScalar();
            kq = result.ToString();

            connection.Close();
            return convertObj(kq);

        }
        public object getById(string function,  Guid id)
        {



            string kq;
            SqlConnection connection = new SqlConnection(connectionString);


            SqlCommand command = new SqlCommand(function + "(@id)", connection);

            command.CommandType = CommandType.Text; // hoặc CommandType.Text nếu bạn gọi một truy vấn SQL bình thường
                                                    // Đặt tham số và giá trị tham số (nếu có)
           

            command.Parameters.Add(new SqlParameter("@id", id));
            //command.Parameters.Add(new SqlParameter("@ParameterName", d));

            connection.Open();

            // ExecuteScalar() để lấy giá trị số lượng nhân viên
            var result = command.ExecuteScalar();
            kq = result.ToString();

            connection.Close();
            return convertObj(kq);

        }
        public object getById(string function,int  id)
        {



            string kq;
            SqlConnection connection = new SqlConnection(connectionString);


            SqlCommand command = new SqlCommand(function + "(@id)", connection);

            command.CommandType = CommandType.Text; // hoặc CommandType.Text nếu bạn gọi một truy vấn SQL bình thường
                                                    // Đặt tham số và giá trị tham số (nếu có)


            command.Parameters.Add(new SqlParameter("@id", id));
            //command.Parameters.Add(new SqlParameter("@ParameterName", d));

            connection.Open();

            // ExecuteScalar() để lấy giá trị số lượng nhân viên
            var result = command.ExecuteScalar();
            kq = result.ToString();

            connection.Close();
            return convertObj(kq);

        }
        public object getById(string function, string user, int id)
        {



            string kq;
            SqlConnection connection = new SqlConnection(connectionString);


            SqlCommand command = new SqlCommand(function + "(@user,@id)", connection);

            command.CommandType = CommandType.Text; // hoặc CommandType.Text nếu bạn gọi một truy vấn SQL bình thường
                                                    // Đặt tham số và giá trị tham số (nếu có)
            command.Parameters.Add(new SqlParameter("@user", user));

            command.Parameters.Add(new SqlParameter("@id", id));
            //command.Parameters.Add(new SqlParameter("@ParameterName", d));

            connection.Open();

            // ExecuteScalar() để lấy giá trị số lượng nhân viên
            var result = command.ExecuteScalar();
            kq = result.ToString();

            connection.Close();
            return convertObj(kq);

        }
    }
}
