using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Weapons;

public class JsonWandRepository(IGameDatabase database)
    : BaseRepo<WandEntity>(database, db => db.Wands), IWandRepository;
