// Learn more about F# at http://fsharp.org

open System
open Microsoft.ML
open Microsoft.ML.Data

[<CLIMutable>]
type ModelInput = {
    [<LoadColumn(1)>]
    Title:string
    [<LoadColumn(2)>]
    Url:string
    [<LoadColumn(3)>]
    Publisher:string
    [<LoadColumn(4)>]
    Category:string
    [<LoadColumn(6)>]
    Hostname:string
}

[<CLIMutable>]
type ModelOutput = {
    PredictedLabel: string
}

[<EntryPoint>]
let main argv =

    let mlContext = MLContext()

    let data = mlContext.Data.LoadFromTextFile<ModelInput>("data/newsCorpora.csv")    

    let datasets = mlContext.Data.TrainTestSplit(data,testFraction=0.1)

    let preProcessingPipeline = 
        EstimatorChain()
            .Append(mlContext.Transforms.Text.FeaturizeText("FeaturizedTitle","Title"))
            .Append(mlContext.Transforms.Text.FeaturizeText("FeaturizedUrl","Url"))
            .Append(mlContext.Transforms.Text.FeaturizeText("FeaturizedPublisher","Publisher"))
            .Append(mlContext.Transforms.Text.FeaturizeText("FeaturizedHost","Hostname"))
            .Append(mlContext.Transforms.Concatenate("Features",[|"FeaturizedTitle"; "FeaturizedUrl" ;"FeaturizedPublisher"; "FeaturizedHost"|]))
            .Append(mlContext.Transforms.Conversion.MapValueToKey("Label","Category"))

    let algorithm = 
        mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy()

    let postProcessingPipeline = 
        mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel")

    let trainingPipeline = 
        preProcessingPipeline
            .Append(algorithm)
            .Append(postProcessingPipeline)

    let model =
        datasets.TrainSet |> trainingPipeline.Fit

    let metrics = 
        (datasets.TestSet |> model.Transform)
        |> mlContext.MulticlassClassification.Evaluate

    printfn "Log Loss: %f | MacroAccuracy: %f" metrics.LogLoss metrics.MacroAccuracy

    let predictions = 
        [
            { 
                Title="A FIRST LOOK AT SURFACE DUO, MICROSOFT’S FOLDABLE ANDROID PHONE"
                Url="https://www.theverge.com/2019/10/3/20895268/microsoft-surface-duo-foldable-phone-dual-screen-android-hands-on-features-price-photos-video"
                Publisher="The Verge"
                Hostname="www.theverge.com" 
                Category = "" 
            }
            { 
                Title="This Shrinking Economy With Low Inflation Is Stuck on Rates"
                Url="https://www.bloomberg.com/news/articles/2019-12-12/when-a-shrinking-economy-and-low-inflation-don-t-mean-rate-cuts?srnd=economics-vp"
                Publisher="Bloomberg"
                Hostname="www.bloomberg.com" 
                Category = "" 
            }
        ] 
        |> mlContext.Data.LoadFromEnumerable
        |> model.Transform
 
    mlContext.Data.CreateEnumerable<ModelOutput>(predictions,false)
    |> Seq.iter(fun prediction -> printfn "Predicted Value: %s" prediction.PredictedLabel)

    0 // return an integer exit code