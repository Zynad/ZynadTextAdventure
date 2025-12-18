using Domain.Database;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;
public class StaffRepository(IGameDatabase database) : BaseRepo<StaffEntity>(database, db => db.Staff), IStaffRepository;
