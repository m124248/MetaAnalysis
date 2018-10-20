library("robumeta")
library("metafor")
library(plyr)
library("dplyr")
library(R2HTML)
args <- commandArgs()
metaAnalysisFilepath <- args[2]

funnel(res, xlab = "Correlation coefficient")
wd <- getwd()
imagedir <- paste(Sys.getenv("HOME"), "C:/MetaAnalysis/MetaAnalysis/wwwroot/html/", sep = "")
setwd(imagedir)
filename <- 'funnel.png'
if (file.exists(filename)) file.remove(filename)
png(funnel, file = filename, width = 9, height = 4)
setwd(wd)