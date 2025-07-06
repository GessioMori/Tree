using Tree.Core.Domain;
using Tree.Persistence.JSON;

namespace Tree.Tests.Persistence.JSON
{
    public class JsonTreeRepositoryTests : IDisposable
    {
        private readonly string _tempFilePath;

        public JsonTreeRepositoryTests()
        {
            this._tempFilePath = Path.Combine(Path.GetTempPath(), $"tree_{Guid.NewGuid()}.json");
        }

        public void Dispose()
        {
            if (File.Exists(this._tempFilePath))
            {
                File.Delete(this._tempFilePath);
            }
        }

        [Fact]
        public async Task SaveAsync_ShouldCreateFile()
        {
            // Arrange
            Component tree = new TreeBuilder("Root")
                .AddNode("Section A")
                    .AddLeaf("Leaf 1", "Text 1")
                .EndNode()
                .Build();

            JsonTreeRepository repository = new();

            // Act
            await repository.SaveAsync(tree, _tempFilePath);

            // Assert
            Assert.True(File.Exists(_tempFilePath));
            string content = await File.ReadAllTextAsync(_tempFilePath);
            Assert.False(string.IsNullOrWhiteSpace(content));
            Assert.Contains("Root", content);
            Assert.Contains("Leaf 1", content);
        }

        [Fact]
        public async Task LoadAsync_ShouldRestoreTreeCorrectly()
        {
            // Arrange
            Component originalTree = new TreeBuilder("Root")
                .AddNode("Section A")
                    .AddLeaf("Leaf 1", "Text 1")
                .EndNode()
                .Build();

            JsonTreeRepository repository = new();

            await repository.SaveAsync(originalTree, _tempFilePath);

            // Act
            Component restoredTree = await repository.LoadAsync(_tempFilePath);

            // Assert
            Assert.NotNull(restoredTree);
            Assert.Equal("Root", restoredTree.Name);

            Branch composite = Assert.IsType<Branch>(restoredTree);
            Assert.Single(composite.Children);

            Component section = composite.Children[0];
            Assert.Equal("Section A", section.Name);

            Branch sectionComposite = Assert.IsType<Branch>(section);
            Assert.Single(sectionComposite.Children);

            Component leaf = sectionComposite.Children[0];
            Assert.IsType<Leaf>(leaf);
            Assert.Equal("Leaf 1", leaf.Name);
            Assert.Equal("Text 1", ((Leaf)leaf).Text);
        }
    }
}