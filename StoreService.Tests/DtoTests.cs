using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StoreService.Web.Models;
using System.Threading.Tasks;
using AutoMapper;
using StoreService.Web.DTO;

namespace StoreService.Tests
{
    class DtoTests
    {
        private MappingProfile profile;
        private MapperConfiguration config;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            profile = new MappingProfile();
            config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = config.CreateMapper();
        }

        [Test]
        public void MapperConfigurationIsOK()
        {
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void MapAuthorTest()
        {
            var author = new Author
            {
                AuthorId = 1,
                AuthorName = "Test Author",
                ProgramBooks = new List<ProgramBook>
                {
                    new ProgramBook { AuthorId = 1, ProgramBookId = 1}
                }
            };

            var dto = mapper.Map<AuthorDto>(author);
            Assert.AreEqual(1, dto.ProgramBooks.Count);
            Assert.AreEqual(dto.AuthorName, author.AuthorName);
            var rev = mapper.Map<Author>(dto);
            Assert.AreEqual(1, rev.ProgramBooks.Count);
            Assert.AreEqual(rev.AuthorName, author.AuthorName);
        }
    }
}
