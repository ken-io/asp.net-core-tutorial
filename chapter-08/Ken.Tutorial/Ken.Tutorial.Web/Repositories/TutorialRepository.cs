using System;
using System.Collections.Generic;
using System.Linq;
using Ken.Tutorial.Web.Models;

namespace Ken.Tutorial.Web.Repositories
{
    public class TutorialRepository
    {
        private TutorialDbContext DbContext { get; }

        public TutorialRepository(TutorialDbContext dbContext)
        {
            //在构造函数中注入DbContext
            this.DbContext = dbContext;
        }

        //添加
        public int Add(UserEntity user)
        {
            using (DbContext)
            {
                //由于我们在UserEntity.Id配置了自增列的Attribute，EF执行完成后会自动把自增列的值赋值给user.Id
                DbContext.Users.Add(user);
                return DbContext.SaveChanges();
            }

        }

        //删除
        public int Delete(int id)
        {
            using (DbContext)
            {
                var userFromContext = DbContext.Users.FirstOrDefault(u => u.Id == id);
                DbContext.Users.Remove(userFromContext);
                return DbContext.SaveChanges();
            }
        }

        //更新
        public int Update(UserEntity user)
        {
            using (DbContext)
            {
                var userFromContext = DbContext.Users.FirstOrDefault(u => u.Id == user.Id);
                userFromContext.Name = user.Name;
                userFromContext.Age = user.Age;
                userFromContext.Hobby = user.Hobby;
                return DbContext.SaveChanges();
            }
        }

        //查询
        public UserEntity QueryById(int id)
        {
            using (DbContext)
            {
                return DbContext.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        //查询集合
        public List<UserEntity> QueryByAge(int age)
        {
            using (DbContext)
            {
                return DbContext.Users.Where(u => u.Age == age).ToList();
            }
        }

        //查看指定列
        public List<string> QueryNameByAge(int age)
        {
            using (DbContext)
            {
                return DbContext.Users.Where(u => u.Age == age).Select(u => u.Name).ToList();
            }
        }

        //分页查询
        public List<UserEntity> QueryUserPaging(int pageSize, int page)
        {
            using (DbContext)
            {
                return DbContext.Users.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
        }

        //事务：将年龄<0的用户修改年龄为0
        public int FixAge()
        {
            using (DbContext)
            {
                using (var transaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userListFromContext = DbContext.Users.Where(u => u.Age < 0);
                        foreach (UserEntity u in userListFromContext)
                        {
                            u.Age = 0;
                        }
                        var count = DbContext.SaveChanges();
                        transaction.Commit();
                        return count;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}