module Helper

open System
open System.Text.RegularExpressions

let splitTextIntoSentences text =
    Regex.Split(text, @"(?<=([\.!\?])|(\.{3}))\s+")

let splitSentenceIntoWords (sen: string) =
    sen.Split(@" .,;-!?:()""" |> Array.ofSeq, StringSplitOptions.RemoveEmptyEntries)

let intersect first second =
    let wordsFirst = splitSentenceIntoWords first
                        |> Array.map (fun w -> w.ToLower())
                        |> Set.ofArray

    let wordsSecond = splitSentenceIntoWords second
                        |> Array.map (fun w -> w.ToLower())
                        |> Set.ofArray

    match wordsFirst.Count,wordsSecond.Count with
    | (0,_) | (_,0) -> 0
    | _ ->
        let common = wordsFirst
                        |> Set.filter (fun w -> wordsSecond |> Set.contains w)
                        |> Set.count

        common / ((wordsFirst.Count+wordsSecond.Count)/2)

let buildSynopsis (text: string) (sentenceCount: int) =
    let sentences = splitTextIntoSentences text
    sentences
    |> Array.mapi (fun i s -> i,s,(sentences
                                   |> Array.fold (fun state t -> state+(intersect s t)) -1))
    |> Array.sortByDescending (fun (_,_,score) -> score)
    |> Array.take sentenceCount
    |> Array.sortBy (fun (i,_,_) -> i)
    |> Array.map (fun (_,s,_) -> s)
