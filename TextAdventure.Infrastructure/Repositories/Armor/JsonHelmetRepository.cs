using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonHelmetRepository(IGameDatabase database)
    : BaseRepo<HelmetEntity>(database, db => db.Helmets), IHelmetRepository;
