using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class LegsRepository(IGameDatabase database) : BaseRepo<LegsEntity>(database, db => db.Legs), ILegsRepository;
