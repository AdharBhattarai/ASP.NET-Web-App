using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace ResturantWebApp.Models
{
    //References Taken from Professor Holmes from Database Access Layer.
    public static class DAL
    {
        //static string connectionString = "Data Source=SQL5041.site4now.net;Initial Catalog=DB_A4E10E_HimalayanFlames;User Id=DB_A4E10E_HimalayanFlames_admin;Password=Nepal123!;";

        static string connectionString = @"Data Source=LAPTOP-0Q3H33KU\SQLEXPRESS;Initial Catalog=HimalayanFlames;Integrated Security=True";

        #region Category Database Access Layer

        //GET All Category from the Database
        public static List<Category> CategoryGet()
        {
            List<Category> categoryList = new List<Category>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocCategoryGetAll";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Category cat = new Category();
                    cat.ID = (int)dr["CategoryID"];
                    cat.Name = (string)dr["Name"];
                    cat.Description = (string)dr["Description"];

                    categoryList.Add(cat);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return categoryList;
        }

        public static int CategoryDelete(Category category)
        {
            if (category == null) return -1;

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_CategoryRemove";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CategoryID", category.ID);
                connection.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return -1;
        }

        public static int MenuDelete(Menu menu)
        {
            if (menu == null) return -1;

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_MenuRemove";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MenuID", menu.ID);
                connection.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return -1;
        }



        public static Category CategoryGet(String idstring, Boolean retNewObject)
        {
            Category retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID))
            {
                if (ID == -1 && retNewObject)
                {
                    retObject = new Category();
                    retObject.ID = -1;
                }
                else if (ID >= 0)
                {
                    retObject = CategoryGet(ID);
                }
            }
            return retObject;
        }


        public static Menu MenuGet(String idstring, Boolean retNewObject)
        {
            Menu retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID))
            {
                if (ID == -1 && retNewObject)
                {
                    retObject = new Menu();
                    retObject.ID = -1;
                }
                else if (ID >= 0)
                {
                    retObject = MenuGet(ID);
                }
            }
            return retObject;
        }
        /// <summary>
        //Gets Category by the ID.
        public static Category CategoryGet(int id)
        {
            Category categoryObject = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocCategoryGet";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CategoryID", id);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        categoryObject = new Category();
                        categoryObject.ID = (int)dr["CategoryID"];
                        categoryObject.Name = (string)dr["Name"];
                        categoryObject.Description = (string)dr["Description"].ToString();
                    }
            }

            catch (Exception ex)
            {
                categoryObject = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return categoryObject;
        }

        // Create Category to database
        public static int CategoryAdd(Category category)
        {

            SqlConnection connection = null;
            int retInt = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_CategoryAdd";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter retParameter = new SqlParameter();
                retParameter = comm.Parameters.AddWithValue("@CategoryID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                retInt = (int)retParameter.Value;
                comm.Parameters.AddWithValue("@Name", category.Name);
                comm.Parameters.AddWithValue("@Description", category.Description);
                connection.Open();
                comm.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retInt;
        }

        // Update the category in the database.
        public static int CategoryUpdate(Category category)
        {

            SqlConnection connection = null;
            int retInt = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_CategoryUpdate";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                comm.Parameters.AddWithValue("@CategoryID", category.ID);
                comm.Parameters.AddWithValue("@Name", category.Name);
                comm.Parameters.AddWithValue("@Description", category.Description);
                comm.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retInt;
        }
        #endregion

        #region Menu Database Access Layer

        //Menu GET everything from the database.
        public static List<Menu> MenuGet()
        {
            List<Menu> retMenu = new List<Menu>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocMenuGetAll";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Menu menu = new Menu();
                    menu.ID = (int)dr["MenuID"];
                    menu.Name = (string)dr["Name"];
                    menu.Description = (string)dr["Description"];
                    menu.Price = (decimal)dr["Price"];
                    menu.CategoryID = (int)dr["CategoryID"];
                    retMenu.Add(menu);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retMenu;
        }

        //GET Menu BY ID
        public static Menu MenuGet(int id)
        {
            Menu retMenuObj = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocMenuGet";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MenuID", id);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        retMenuObj = new Menu();
                        retMenuObj.ID = (int)dr["MenuID"];
                        retMenuObj.Name = (string)dr["Name"];
                        retMenuObj.Description = (string)dr["Description"];
                        retMenuObj.Price = (decimal)dr["Price"];
                        retMenuObj.CategoryID = (int)dr["CategoryID"];
                    }
            }

            catch (Exception ex)
            {
                retMenuObj = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retMenuObj;
        }

        //Creates the menu in the database.
        public static int MenuAdd(Menu menu)
        {

            SqlConnection connection = null;
            int retInt = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_MenuAdd";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter retParameter = new SqlParameter();
                retParameter = comm.Parameters.AddWithValue("@MenuID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                retInt = (int)retParameter.Value;
                //comm.Parameters.AddWithValue("@MenuID", menu.ID);
                comm.Parameters.AddWithValue("@Name", menu.Name);
                comm.Parameters.AddWithValue("@Description", menu.Description);
                comm.Parameters.AddWithValue("@Price", menu.Price);
                comm.Parameters.AddWithValue("@CategoryID", menu.CategoryID);


                connection.Open();
                comm.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retInt;
        }

        //Get list of all Menu for CategoryID
        public static List<Menu> MenuGet(Category category)
        {
            List<Menu> retMenuList = new List<Menu>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocMenuGetForCategory";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CategoryID", category.ID);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        Menu menu = new Menu();
                        menu.ID = (int)dr["MenuID"];
                        menu.Name = (string)dr["Name"];
                        menu.Description = (string)dr["Description"];
                        menu.Price = (decimal)dr["Price"];
                        menu.CategoryID = (int)dr["CategoryID"];
                        retMenuList.Add(menu);
                        //retObject.ID = (int)dr["MenuID"];
                    }
            }

            catch (Exception ex)
            {
                retMenuList = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retMenuList;
        }

        public static int MenuUpdate(Menu menu)
        {

            SqlConnection connection = null;
            int retInt = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_MenuUpdate";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                comm.Parameters.AddWithValue("@MenuID", menu.ID);
                comm.Parameters.AddWithValue("@Name", menu.Name);
                comm.Parameters.AddWithValue("@Description", menu.Description);
                comm.Parameters.AddWithValue("@Price", menu.Price);
                comm.Parameters.AddWithValue("@CategoryID", menu.CategoryID);


                comm.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retInt;
        }

        #endregion

        #region Role Database Access Layer

        public static Role RoleGet(int id)
        {
            Role retObject = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocRoleGet";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@RoleID", id);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        retObject = new Role();
                        retObject.ID = (int)dr["RoleID"];
                        retObject.Name = (string)dr["Name"];
                        retObject.CategoryAdd = (bool)dr["CategoryAdd"];
                        retObject.CategoryEdit = (bool)dr["CategoryEdit"];
                        retObject.CategoryDelete = (bool)dr["CategoryDelete"];
                        retObject.CategoryIndex = (bool)dr["CategoryIndex"];
                        retObject.MenuCreate = (bool)dr["MenuCreate"];
                        retObject.MenuDelete = (bool)dr["MenuDelete"];
                        retObject.MenuEdit = (bool)dr["MenuEdit"];
                        retObject.OrderIndex = (bool)dr["OrderIndex"];
                    }
            }
            catch (Exception ex)
            {
                retObject = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retObject;
        }
        #endregion

        #region User Database Access Layer

        //Get Every User from the database.
        public static List<User> UserGet()
        {
            List<User> retListUser = new List<User>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocUserGetAll";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    User user = new User();
                    user.ID = (int)dr["UserID"];
                    user.FirstName = (string)dr["FirstName"];
                    user.LastName = (string)dr["LastName"];
                    user.Email = (string)dr["Email"];
                    user.Password = (string)dr["Password"];
                    user.Salt = (string)dr["Salt"];
                    retListUser.Add(user);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retListUser;
        }

        //Get User by ID
        public static User UserGetByID(int id)
        {
            User retUserObject = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocUserGet";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@UserID", id);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    retUserObject = new User();
                    retUserObject.ID = (int)dr["UserID"];
                    retUserObject.FirstName = (string)dr["FirstName"];
                    retUserObject.LastName = (string)dr["LastName"];
                    retUserObject.Email = (string)dr["Email"];
                    retUserObject.Password = (string)dr["Password"];
                    retUserObject.Phone = (string)dr["Phone"];
                    retUserObject.RoleID = (int)dr["RoleID"];

                    retUserObject.Salt = (string)dr["Salt"];
                }
            }

            catch (Exception ex)
            {
                retUserObject = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retUserObject;
        }

        // Get User by Username and Password
        public static User UserGet(string uName, string pWord)
        {
            User retUserObject = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocUserGetByEmail";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Email", uName);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        retUserObject = new User();
                        retUserObject.ID = (int)dr["UserID"];
                        retUserObject.Email = (string)dr["Email"];
                        retUserObject.Password = (string)dr["Password"];
                        retUserObject.Salt = (string)dr["Salt"];

                        if (uName != null)
                        {
                            if (retUserObject.Password == Models.Hash.HashIt(pWord, retUserObject.Salt))
                            {
                                // Password matched.
                            }
                            else
                            {
                                // Passoword does not match.
                                retUserObject.Email = null;
                            }
                        }
                        return retUserObject;
                    }
            }

            catch (Exception ex)
            {
                retUserObject = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retUserObject;
        }

        // Create the User in the database
        public static int UserAdd(User user)
        {
            SqlConnection connection = null;
            int retInt = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_UserAdd";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter retParameter = new SqlParameter();
                retParameter = comm.Parameters.AddWithValue("@UserID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                retInt = (int)retParameter.Value;
                comm.Parameters.AddWithValue("@FirstName", user.FirstName);
                comm.Parameters.AddWithValue("@LastName", user.LastName);
                comm.Parameters.AddWithValue("@Email", user.Email);
                comm.Parameters.AddWithValue("@Password", user.Password);
                comm.Parameters.AddWithValue("@Phone", user.Phone);
                comm.Parameters.AddWithValue("@RoleID", user.RoleID);
                comm.Parameters.AddWithValue("@Salt", user.Salt);

                connection.Open();
                comm.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retInt;
        }

        // Get User Cookie
        public static string GetCookie(User usr)
        {
            return usr.Salt + usr.ID;
        }

        // Check Cookie value
        public static User GetUserForCookie(string cookie)
        {
            User usr = null;

            if (!string.IsNullOrEmpty(cookie))
            {
                // we have a cookie set
                int sePlace = cookie.LastIndexOf("=") + 1;
                string saltCheck = cookie.Substring(0, sePlace);
                string strID = cookie.Substring(sePlace);
                int id;
                if (int.TryParse(strID, out id))
                {
                    usr = DAL.UserGetByID(id);
                    if (usr.Salt == saltCheck)
                    {
                        // Matched.
                    }
                    else
                    {
                        // Not Matched ID or Salt.
                        usr = null;
                    }
                }
            }
            return usr;
        }
        #endregion

        #region OrderStatus Database Access Layer

        // Get everything OrderStatus from the Database
        public static List<OrderStatus> OrderStatusGet()
        {
            List<OrderStatus> retOrderList = new List<OrderStatus>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocOrderStatusGetAll";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    OrderStatus order = new OrderStatus();
                    order.ID = (int)dr["OrderStatusID"];
                    order.UserID = (int)dr["UserID"];
                    order.OrderTime = (DateTime)dr["OrderTime"];
                    order.MenuOrders = DAL.MenuOrderGet(order);
                    order.User = DAL.UserGetByID((int)dr["UserID"]);
                    //DAL.OrderStatusGet()

                    retOrderList.Add(order);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retOrderList;
        }

        //Get OrderStatus by ID
        public static OrderStatus GetOrderByID(int id)
        {
            OrderStatus retObject = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocOrderStatusGet";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@OrderStatusID", id);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        retObject = new OrderStatus();
                        retObject.ID = (int)dr["OrderStatusID"];
                        retObject.OrderTime = (DateTime)dr["OrderTime"];
                    }
            }

            catch (Exception ex)
            {
                retObject = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retObject;
        }

        // Create Order in the database
        public static int OrderStatusAdd(OrderStatus order)
        {

            SqlConnection connection = null;
            int retInt = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_OrderStatusAdd";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter retParameter = new SqlParameter();
                retParameter = comm.Parameters.AddWithValue("@OrderStatusID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                //comm.Parameters.AddWithValue("@MenuID", menu.ID);
                comm.Parameters.AddWithValue("@OrderTime", order.OrderTime);
                comm.Parameters.AddWithValue("@UserID", order.UserID);
                connection.Open();
                comm.ExecuteNonQuery();
                retInt = (int)comm.Parameters["@OrderStatusID"].Value;
                //retInt = (int)comm.ExecuteScalar();
                var orderStatusID = retInt;
                // adding menu items
                foreach (var menuOrder in order.MenuOrders)
                {
                    menuOrder.OrderStatusID = orderStatusID;
                    MenuOrderAdd(menuOrder);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retInt;
        }


        // Get Every Order by User
        public static List<OrderStatus> OrderGetByUser(User user)
        {
            List<OrderStatus> retObject = new List<OrderStatus>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocOrderStatusGetForUser";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@UserID", user.ID);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        OrderStatus order = new OrderStatus();
                        order.ID = (int)dr["OrderStatusID"];
                        order.OrderTime = (DateTime)dr["OrderTime"];
                        retObject.Add(order);
                    }
            }

            catch (Exception ex)
            {
                retObject = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retObject;
        }

        #endregion

        #region MenuOrder Database Access Layer

        // Get Everything from the Database.
        public static List<MenuOrder> MenuOrderGet()
        {
            List<MenuOrder> retList = new List<MenuOrder>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocMenuOrderGetAll";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    MenuOrder menuOrder = new MenuOrder();
                    menuOrder.ID = (int)dr["MenuOrderID"];
                    menuOrder.ItemPrice = (decimal)dr["ItemPrice"];
                    menuOrder.Quantity = (int)dr["Quantity"];
                    menuOrder.TotalPrice = (decimal)dr["TotalPrice"];
                    menuOrder.OrderStatusID = (int)dr["OrderStatusID"];
                    menuOrder.MenuID = (int)dr["MenuID"];
                    menuOrder.Comment = (string)dr["Comment"];

                    retList.Add(menuOrder);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retList;
        }

        // Get Menuorder by Order Status
        public static List<MenuOrder> MenuOrderGet(OrderStatus orderstatus)
        {
            List<MenuOrder> retObject = new List<MenuOrder>();
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sprocMenuOrderGetForOrderStatus";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@OrderStatusID", orderstatus.ID);

                connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                    while (dr.Read())
                    {
                        MenuOrder menuOrder = new MenuOrder();
                        menuOrder.ID = (int)dr["MenuID"];
                        menuOrder.Quantity = (int)dr["Quantity"];
                        menuOrder.ItemPrice = (decimal)dr["ItemPrice"];
                        menuOrder.OrderStatusID = (int)dr["OrderStatusID"];
                        menuOrder.Comment = (string)dr["Comment"];
                        menuOrder.TotalPrice = (decimal)dr["TotalPrice"];
                        menuOrder.Menu = DAL.MenuGet((int)dr["MenuID"]);
                        retObject.Add(menuOrder);
                        //retObject.ID = (int)dr["MenuID"];
                    }
            }

            catch (Exception ex)
            {
                retObject = null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retObject;
        }

        // MenuOrder Add
        public static int MenuOrderAdd(MenuOrder menuOrder)
        {

            SqlConnection connection = null;
            int retInt = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = "sproc_MenuOrderAdd";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter retParameter = new SqlParameter();
                retParameter = comm.Parameters.AddWithValue("@MenuOrderID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                retInt = (int)retParameter.Value;
                comm.Parameters.AddWithValue("@Quantity", menuOrder.Quantity);
                comm.Parameters.AddWithValue("@ItemPrice", menuOrder.ItemPrice);
                comm.Parameters.AddWithValue("@TotalPrice", menuOrder.TotalPrice);
                comm.Parameters.AddWithValue("@Comment", menuOrder.Comment);
                comm.Parameters.AddWithValue("@MenuID", menuOrder.MenuID);
                comm.Parameters.AddWithValue("@OrderStatusID", menuOrder.OrderStatusID);

                connection.Open();
                comm.ExecuteNonQuery();
            }


            catch (Exception ex)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return retInt;
        }
        #endregion
    }
}

