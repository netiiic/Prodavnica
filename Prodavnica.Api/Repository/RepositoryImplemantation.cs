﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodavnica.Api.Dto;
using Prodavnica.Api.Infrastructure;
using Prodavnica.Api.Interfaces;
using Prodavnica.Api.Models;

namespace Prodavnica.Api.Repository
{
    public class RepositoryImplemantation : IRepository
    {
        private readonly IMapper _mapper;
        private readonly ProdavnicaDbContext _dbContext;

        public RepositoryImplemantation(IMapper mapper, ProdavnicaDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public UserDto ChangeProfile(Guid id, UserDto userDto)
        {
            User user = _dbContext.Users.Find(id);
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.FullName = userDto.FullName;
            user.DateOfBirth = userDto.DateOfBirth;
            user.Address = userDto.Address;
            user.Image = userDto.Image;
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public List<UserDto> GetAllUnverified()
        {
            List<User> users = _dbContext.Users.ToList();
            List<User> unverified = new();
            foreach (User user in users)
            {
                if (user.UserType == Models.UserType.Seller && user.Verified == false)
                {
                    unverified.Add(user);
                }
            }

            return _mapper.Map<List<UserDto>>(unverified);
        }

        public UserDto RegisterUser(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(userDto);
        }

        public UserDto UserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Verify(string username, bool verify)
        {
            List<User> users = _dbContext.Users.ToList();
            foreach(User u in users)
            {
                if(u.Username == username)
                {
                    u.Verified = verify;
                    _dbContext.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public bool UserExists(string username)
        {
            return _dbContext.Users.Any(c => c.Username == username);
        }

        public UserDto GetUser(string username)
        {
            User user = _dbContext.Users.SingleOrDefault(c => c.Username == username);
            return _mapper.Map<UserDto>(user);
        }

        public bool AuthenticateUser(string username, string password)
        {
            return _dbContext.Users.Any(c => c.Username == username && c.Password == password);
        }

        public int GetUserType(Guid id)
        {
            User user = _dbContext.Users.Find(id);
            return (int)user.UserType;
        }

        public ShoppingItemDto AddNewItemToDB(ShoppingItemDto newItem)
        {
            ShoppingItem newShoppingItem = _mapper.Map<ShoppingItem>(newItem);
            _dbContext.ShoppingItems.Add(newShoppingItem);
            _dbContext.SaveChanges();

            return _mapper.Map<ShoppingItemDto>(newShoppingItem);
        }

        public bool UserExistsByGuid(Guid id)
        {
            return _dbContext.Users.Any(c => c.Id == id);
        }

        public List<ShoppingItemDto> GetSellerItems(Guid sellerId)
        {
            List<ShoppingItem> allItems = _dbContext.ShoppingItems.ToList();
            List<ShoppingItem> fromOneSeller = new();

            foreach (ShoppingItem item in allItems)
            {
                if (item.SellerId == sellerId && item.Bought == false)
                {
                    fromOneSeller.Add(item);
                }
            }

            return _mapper.Map<List<ShoppingItemDto>>(fromOneSeller);

        }

        public ShoppingItemDto UpdateItem(Guid id, ShoppingItemDto shoppingItem)
        {
            ShoppingItem item = _dbContext.ShoppingItems.Find(id);
            item.Name = shoppingItem.Name;
            item.Price = shoppingItem.Price;
            item.Quantity = shoppingItem.Quantity;
            item.Description = shoppingItem.Description;
            item.Image = shoppingItem.Image;
            item.SellerId = shoppingItem.SellerId;
            _dbContext.SaveChanges();

            return _mapper.Map<ShoppingItemDto>(item);
        }

        public bool DeleteItem(Guid id)
        {
            if (ItemExists(id))
            {
                _dbContext.ShoppingItems.Remove(_dbContext.ShoppingItems.Find(id));
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool ItemExists(Guid id)
        {
            if(_dbContext.ShoppingItems.Find(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ShoppingItemDto> GetAllItems()
        {
            List<ShoppingItem> shoppingItems = _dbContext.ShoppingItems.ToList();
            List<ShoppingItem> ret = new();
            foreach (ShoppingItem item in shoppingItems)
            {
                if(item.Bought == false && item.Quantity > 0)
                {
                    ret.Add(item);
                }
            }
            return _mapper.Map<List<ShoppingItemDto>>(ret);
        }

        public OrederDto MakeOrder(OrederDto order)
        {
            ReduceQuantityOfItem(order);
            Oreder newOrder = _mapper.Map<Oreder>(order);
            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();

            return _mapper.Map<OrederDto>(newOrder);
        }



        //ne radi kako treba
        private void ReduceQuantityOfItem(OrederDto order)
        {
            List<ShoppingItem> shoppingItemDB = _dbContext.ShoppingItems.ToList();
            List<ShoppingItemDto> fromOrder = order.Items;
            foreach (ShoppingItem itemDB in shoppingItemDB)
            {
                if (itemDB.Bought == false)
                {
                    foreach (ShoppingItemDto itemFO in fromOrder)
                    {
                        if (itemFO.Procesed == false)
                        {
                            if (itemDB.Id == itemFO.BoughtId)
                            {
                                itemDB.Quantity -= itemFO.Quantity;
                                itemFO.Procesed = true;
                            }
                        }
                    }

                }
            }
        }



        public List<OrederDto> UserFinalizedPurchases(Guid userId)
        {
            List<Oreder> returnOrders = new();
            List<Oreder> ordersFromDB = _dbContext.Orders.ToList();
            List<ShoppingItem> itemsFromDB = _dbContext.ShoppingItems.ToList();
            List<ShoppingItem> itemsInOrder = new();
            foreach(Oreder order in ordersFromDB)
            {
                if(order.ByerId == userId && order.Finalized == true)
                {
                    Guid tmp = order.Id;
                    itemsInOrder = _dbContext.Orders
                                    .Where(g => g.Id == tmp)
                                    .SelectMany(g => g.Items)
                                    .ToList();

                    order.Items = itemsInOrder;
                    returnOrders.Add(order);
                }
            }

            return _mapper.Map<List<OrederDto>>(returnOrders);
        }

        public List<OrederDto> GetAllOrders()
        {
            var returnOrders = new List<OrederDto>();

            var ordersFromDB = _dbContext.Orders.Include(o => o.Items).ToList();

            foreach (var order in ordersFromDB)
            {
                var buyer = _dbContext.Users.FirstOrDefault(u => u.Id == order.ByerId);
                if (buyer != null)
                {
                    var orderDto = _mapper.Map<OrederDto>(order);
                    orderDto.ByerFullName = buyer.FullName;
                    returnOrders.Add(orderDto);
                }
            }

            return returnOrders;
        }


        public List<OrederDto> GetSellerOrders(Guid sellerID)
        {
            var returnOrders = new List<OrederDto>();

            var ordersFromDB = _dbContext.Orders.Include(o => o.Items)
                                               .Where(o => o.Items.Any(item => item.SellerId == sellerID))
                                               .ToList();

            foreach (var order in ordersFromDB)
            {
                var buyer = _dbContext.Users.FirstOrDefault(u => u.Id == order.ByerId);
                if (buyer != null)
                {
                    var sellerItemsInOrder = order.Items.Where(item => item.SellerId == sellerID).ToList();
                    var orderDto = _mapper.Map<OrederDto>(order);
                    orderDto.ByerFullName = buyer.FullName;
                    orderDto.Items = _mapper.Map<List<ShoppingItemDto>>(sellerItemsInOrder);
                    returnOrders.Add(orderDto);
                }
            }

            return returnOrders;
        }



    }
}
