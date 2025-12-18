using Domain.Database;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;
public class SwordRepository(IGameDatabase database)
    : BaseRepo<SwordEntity>(database, db => db.Swords), ISwordRepository;
