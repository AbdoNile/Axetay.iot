resource "aws_iot_thing_type" "ButonType" {
  name = "Zorrar"
  
}

resource "aws_s3_bucket" "b" {
  bucket = "axetay-thing-certificates"
  acl    = "private"
}