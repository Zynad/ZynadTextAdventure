using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Weapons;

public class JsonStaffRepository(IGameDatabase database)
    : BaseRepo<StaffEntity>(database, db => db.Staff), IStaffRepository;
