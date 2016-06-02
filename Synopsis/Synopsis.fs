module Helper

open System
open System.Text.RegularExpressions

let splitTextInSentences text =
    Regex.Split(text, @"(?<=([\.!\?])|(\.{3}))\s+")