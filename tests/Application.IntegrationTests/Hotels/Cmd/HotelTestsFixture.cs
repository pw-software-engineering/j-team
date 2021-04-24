using HotelReservationSystem.Application.Hotels.Commands.CreateHotel;
using HotelReservationSystem.Application.Offers.Commands.CreateOffer;
using System.Threading.Tasks;
using System.Linq;
using HotelReservationSystem.Application.Rooms.Commands.CreateRoom;
using HotelReservationSystem.Application.Rooms.Queries.GetRoomsWithPagination;
using Application.Rooms;
using HotelReservationSystem.Domain.Entities;
using System.Threading;
using System;
using HotelReservationSystem.Application.Common.Interfaces;

namespace HotelReservationSystem.Application.IntegrationTests
{

    using static Testing;

    class HotelTestsFixture
    {
        private readonly IApplicationDbContext context;
        public HotelTestsFixture(IApplicationDbContext context)
        {
            this.context = context;

        }
        private async Task<Room> CreateRoom(string number)
        {
            var hotelId = await SendAsync(new CreateHotelCmd
            {
                Name = "hotel1",
                City = "city",
                Country = "country",
                Password = "hotel1"
            });

            var roomId = await SendAsync(new CreateRoomCmd
            {
                HotelRoomNumber = number,
                HotelID = hotelId,
            });
            var room = context.Rooms.Find(roomId);
            return room;
        }
        private async Task<Offer> CreateOffer(int hotelId)
        {
            var offerId = await SendAsync(new CreateOfferCmd
            {
                Title = "offer1",
                HotelId = hotelId
            });
            var offer = context.Offers.Find(offerId);
            return offer;
        }
        private async Task<Reservation> CreateReservation(int roomId, Offer offer, DateTime from, DateTime to)
        {
            var client = new Client
            {
                Name = "Client1",
                Surname = "Clientsurname",
                Email = "email@email.com",
                Username = "Client123"
            };
            context.Clients.Add(client);
            await context.SaveChangesAsync(CancellationToken.None);

            Reservation reservation = new Reservation()
            {
                RoomId = roomId,
                OfferId = offer.OfferId,
                FromTime = from,
                ToTime = to,
                ClientId = client.ClientId
            };

            offer.Reservations.Add(reservation);
            await context.SaveChangesAsync(CancellationToken.None);
            return reservation;
        }
        public async Task<RoomDto> CreateRoom(bool withOffer = false, bool withReservation = false, bool isReplaceableInOffer = true)
        {
            DateTime now = DateTime.Now;
            var room = await CreateRoom("R01");
            if (withOffer)
            {
                var offer = await CreateOffer(room.HotelId);

                offer.Rooms.Add(room);
                await context.SaveChangesAsync(CancellationToken.None);

                if (withReservation)
                {
                    var reservation = await CreateReservation(room.RoomId, offer, now.AddDays(-1), now.AddDays(1));

                    var room2 = await CreateRoom("R02");

                    offer.Rooms.Add(room2);
                    await context.SaveChangesAsync(CancellationToken.None);

                    var reservation2 = await CreateReservation(room.RoomId, offer, now.AddDays(-1), now.AddDays(1));

                    if (isReplaceableInOffer)
                    {
                        reservation2.FromTime = now.AddDays(1);
                        reservation2.ToTime = now.AddDays(2);
                    }
                    offer.Reservations.Add(reservation2);
                    await context.SaveChangesAsync(CancellationToken.None);
                    offer.Rooms.Add(room2);
                    await context.SaveChangesAsync(CancellationToken.None);
                }
            }
            var rooms = await SendAsync(new GetRoomsWithPaginationQuery()
            {
                RoomNo = "R01"
            });

            return rooms.Items.First();
        }
    }
}
