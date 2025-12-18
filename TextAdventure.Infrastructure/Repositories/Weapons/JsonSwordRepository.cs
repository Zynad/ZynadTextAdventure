using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Weapons;

public class JsonSwordRepository(IGameDatabase database)
    : BaseRepo<SwordEntity>(database, db => db.Swords), ISwordRepository;
