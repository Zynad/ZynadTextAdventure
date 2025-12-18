using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Weapons;

public class JsonAxeRepository(IGameDatabase database) : BaseRepo<AxeEntity>(database, db => db.Axes), IAxeRepository;
