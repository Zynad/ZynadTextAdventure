using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonLegsRepository(IGameDatabase database) : BaseRepo<LegsEntity>(database, db => db.Legs), ILegsRepository;
