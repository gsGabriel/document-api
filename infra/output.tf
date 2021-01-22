output "url" {
  value = aws_lb.this.dns_name
}
output "s3_bucket_id" {
    value = aws_s3_bucket.s3.id
}
output "s3_bucket_arn" {
    value = aws_s3_bucket.s3.arn
}
output "s3_bucket_domain_name" {
    value = aws_s3_bucket.s3.bucket_domain_name
}
output "rds_address" {
  value = aws_db_instance.postgres.address
}
