#load "./20_datacontext.dothtml.csx"

Unit.SetFileName("ProfileDetail.dothtml");
Unit.SetDefault("./ProfileDetail_20.dothtml");
Unit.SetCorrect("./ProfileDetail_30.dothtml");

inner.GetProperty("@DataContext")
    .HasBinding("Address");
