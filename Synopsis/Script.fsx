#load "Synopsis.fs"
open Helper

open System

let text = IO.File.ReadAllText(...)

buildSynopsis text 10