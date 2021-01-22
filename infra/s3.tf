resource "aws_s3_bucket" "s3" {
    bucket = "gsdocumentapi"
    acl    = "public-read"
}
