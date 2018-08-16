using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    class SocialNetworkInitializer : DropCreateDatabaseIfModelChanges<SocialNetworkContext>
    {
        protected override void Seed(SocialNetworkContext context)
        {
            UserDetails u1 = new UserDetails
            {
                Id = "f044f5d9-0a34-428e-8a2d-c193ba8518e1",
                City = "Toronto",
                Country = "Canada",
                FirstName = "David",
                LastName = "Cooper",
                DateOfBirth = new DateTime(1980, 5, 30)
            };
            UserDetails u2 = new UserDetails
            {
                Id = "c9ad2c9d-1be4-4aba-b341-dd5dba668754",
                City = "Kiev",
                Country = "Ukraine",
                FirstName = "Andiy",
                LastName = "Bedzir",
                DateOfBirth = new DateTime(1970, 12, 1)
            };
            UserDetails u3 = new UserDetails
            {
                Id = "d4eecea7-c09c-4196-a2ae-abadcc0f4c8e",
                City = "Kiev",
                Country = "Ukraine",
                FirstName = "Andiy",
                LastName = "Bedzir",
                DateOfBirth = new DateTime(1970, 12, 1)
            };
            var userList = new List<UserDetails>();
            userList.Add(u1);
            userList.Add(u2);

            context.Users.Add(u3);
            context.Users.AddRange(userList);
            context.SaveChanges();

            Chat c1 = new Chat() { CreateDate = DateTime.Now, Users = userList };
            context.Chats.Add(c1);
            context.SaveChanges();
            Content content1 = new Content() { MessageContent = "Hello world" };
            Content content2 = new Content() { MessageContent = "I'm not a worls I'm  a human!" };
            Content content3 = new Content() { MessageContent = "Sorry than" };

            context.Content.Add(content1);
            context.Content.Add(content2);
            context.Content.Add(content3);
            context.SaveChanges();
            try
            {
                var MessageList = new List<Message>()
            {
                new Message(){ Chat = c1,   CreateDate = DateTime.Now, ModifiedDate = DateTime.Now,  UserFrom= u1, UserTo = u2, Content = content1, },
                new Message(){ Chat = c1,  CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, UserFrom= u2, UserTo = u1, Content = content2, },
                new Message(){ Chat = c1,   CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, UserFrom= u1, UserTo = u2, Content = content3, },

            };
                context.Messages.AddRange(MessageList);

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Study\VSProjects\SocialNetwork\log.txt", true))
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {

                        sw.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sw.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
                throw;
            }
            
            

        }
    }
}
