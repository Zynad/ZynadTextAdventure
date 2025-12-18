using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonGlovesRepository(IGameDatabase database)
    : BaseRepo<GlovesEntity>(database, db => db.Gloves), IGlovesRepository;
