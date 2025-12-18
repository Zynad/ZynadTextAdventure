using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonBootsRepository(IGameDatabase database)
    : BaseRepo<BootsEntity>(database, db => db.Boots), IBootsRepository;
