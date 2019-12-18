terraform {
  backend "s3" {
    key    = "samples"
    region = "us-east-1"
  }
}