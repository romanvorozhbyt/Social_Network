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
                DateOfBirth = new DateTime(1980, 5, 30),
                Friends = new List<UserDetails>()
            };
            UserDetails u2 = new UserDetails
            {
                Id = "c9ad2c9d-1be4-4aba-b341-dd5dba668754",
                City = "Kiev",
                Country = "Ukraine",
                FirstName = "Andiy",
                LastName = "Bedzir",
                DateOfBirth = new DateTime(1970, 12, 1),
                Friends = new List<UserDetails>() 
            };
            UserDetails u3 = new UserDetails
            {
                Id = "d4eecea7-c09c-4196-a2ae-abadcc0f4c8e",
                City = "Kiev",
                Country = "Ukraine",
                FirstName = "Andiy",
                LastName = "Bedzir",
                DateOfBirth = new DateTime(1970, 12, 1),
                Friends = new List<UserDetails>()
            };
            var userList = new List<UserDetails> {u1, u2};
            u1.Friends.Add(u2);
            u1.Friends.Add(u3);
            u2.Friends.Add(u1);
            u3.Friends.Add(u1);
            context.Users.Add(u3);
            context.Users.AddRange(userList);
            context.SaveChanges();

            Chat c1 = new Chat() { CreateDate = DateTime.Now, Users = userList };
            context.Chats.Add(c1);
            context.SaveChanges();
            try
            {
                var MessageList = new List<Message>()
            {
                new Message(){Id = 1, Chat = c1,   CreateDate = DateTime.Now, ModifiedDate = DateTime.Now,  UserFrom= u1, UserFromId = u1.Id},
                new Message(){Id= 2, Chat = c1,  CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, UserFrom= u2, UserFromId = u2.Id},
                new Message(){ Id = 3, Chat = c1,   CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, UserFrom= u1, UserFromId = u2.Id},

            };
                context.Messages.AddRange(MessageList);
                context.SaveChanges();

                Content content1 = new Content() { MessageContent = "Hello world", Message = MessageList[0], Id = MessageList[0].Id };
                Content content2 = new Content() { MessageContent = "I'm not a worls I'm  a human!", Message = MessageList[1], Id = MessageList[1].Id };
                Content content3 = new Content() { MessageContent = "Sorry than", Message = MessageList[2], Id = MessageList[2].Id };
                
                context.Content.Add(content1);
                context.Content.Add(content2);
                context.Content.Add(content3);
                
                MessageList[0].Content = content1;
                MessageList[1].Content = content2;
                MessageList[2].Content = content3;
               
                c1.Messages = MessageList;
                context.SaveChanges();
                List<FriendRequest> frList = new List<FriendRequest>()
                {
                    new FriendRequest()
                    {
                        RequestedBy_Id = u1.Id,
                        RequestTime = DateTime.Now,
                        RequestedTo = u2,
                        RequestedBy = u1,
                        FriendRequestFlag = FriendRequestFlag.Approved
                    },
                    new FriendRequest() {RequestedBy = u2, RequestedBy_Id = u2.Id,RequestedTo = u3, RequestTime = DateTime.Now, FriendRequestFlag = FriendRequestFlag.Pending },
                    new FriendRequest() {RequestedBy_Id = u3.Id, RequestedTo = u1, RequestedBy = u3, RequestTime = DateTime.Now, FriendRequestFlag = FriendRequestFlag.Declined}

                };
                context.Friends.AddRange(frList);
                context.SaveChanges();


            }
            catch (DbEntityValidationException e)
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Study\VSProjects\SocialNetwork\log.txt", false))
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
