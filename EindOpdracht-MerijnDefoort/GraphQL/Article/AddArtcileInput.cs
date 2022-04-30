namespace Store.GraphQL.Mutations;

public record AddArticleInput(string Name, string Description, double Price, int salePercentage, string Image);