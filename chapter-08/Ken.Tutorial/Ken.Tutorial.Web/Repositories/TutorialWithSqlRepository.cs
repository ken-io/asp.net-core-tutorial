using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Ken.Tutorial.Web.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Ken.Tutorial.Web.Repositories
{
    public class TutorialWithSqlRepository
    {
        private TutorialDbContext DbContext { get; }

        public TutorialWithSqlRepository(TutorialDbContext dbContext)
        {
            //在构造函数中注入DbContext
            this.DbContext = dbContext;
        }

        //添加
        public int Add(UserEntity user)
        {
            using (var connection = DbContext.Database.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand() as MySqlCommand;
                command.CommandText = "INSERT INTO user (name,age,hobby) VALUES(@name,@age,@hobby)";
                command.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@name",
                    DbType = DbType.String,
                    Value = user.Name
                });
                command.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@age",
                    DbType = DbType.Int32,
                    Value = user.Age
                });
                command.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@hobby",
                    DbType = DbType.String,
                    Value = user.Hobby
                });
                var count = command.ExecuteNonQuery();
                //获取插入时产生的自增列Id并赋值给user.Id使用
                user.Id = (int)command.LastInsertedId;
                return count;
            }

        }

        //删除
        public int Delete(int id)
        {
            using (DbContext)
            {
                return DbContext.Database.ExecuteSqlCommand(
                    "DELETE FROM user WHERE id={0}", id);
            }
        }

        //更新
        public int Update(UserEntity user)
        {
            using (DbContext)
            {
                return DbContext.Database.ExecuteSqlCommand(
                     "UPDATE user SET name={0}, age={1}, hobby={2} WHERE id={3}",
                      user.Name, user.Age, user.Hobby, user.Id);
            }
        }

        //查询
        public UserEntity QueryById(int id)
        {
            using (DbContext)
            {
                return DbContext.Users.FromSql("SELECT id,name,age,hobby FROM user WHERE id={0}", id).FirstOrDefault();
            }
        }

        //查询集合
        public List<UserEntity> QueryByAge(int age)
        {
            using (DbContext)
            {
                return DbContext.Users.FromSql("SELECT id,name,age,hobby FROM user WHERE age={0}", age).ToList();
            }
        }

        //查看指定列
        public List<string> QueryNameByAge(int age)
        {
            using (DbContext)
            {
                return DbContext.Users.FromSql("SELECT id,name FROM user WHERE age={0}", age).Select(u => u.Name).ToList();
            }
        }

        //分页查询
        public List<UserEntity> QueryUserPaging(int pageSize, int page)
        {
            using (DbContext)
            {
                return DbContext.Users.FromSql("SELECT id,name,age,hobby FROM user LIMIT {0},{1}", pageSize * (page - 1), pageSize).ToList();
            }
        }

        //事务：将年龄<0的用户修改年龄为0
        public int FixAge()
        {
            using (DbContext)
            {
                using (var connection = DbContext.Database.GetDbConnection())
                {
                    //打开连接
                    connection.Open();
                    //开启事务
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            //获取命令对象
                            var command = connection.CreateCommand();
                            command.Transaction = transaction;
                            command.CommandText = "UPDATE user SET age=@age WHERE age<@age";
                            command.Parameters.Add(new MySqlParameter()
                            {
                                ParameterName = "@age",
                                DbType = DbType.Int32,
                                Value = 0
                            });
                            var count = command.ExecuteNonQuery();
                            transaction.Commit();
                            return count;
                        }
                        catch (Exception ex)
                        {
                            connection.Close();
                            transaction.Rollback();
                            return 0;
                        }
                    }

                }
            }
        }
    }
}