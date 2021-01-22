resource "aws_db_subnet_group" "db_subnet_group" {
    subnet_ids  = data.aws_subnet_ids.this.ids
}

resource "aws_db_instance" "postgres" {
  allocated_storage      = 100
  engine                 = "postgres"
  engine_version         = "11.5"
  identifier             = "document-api"
  instance_class         = "db.t2.micro"
  name                   = "document_api"
  password               = "password"
  username               = "postgres"
  db_subnet_group_name   = aws_db_subnet_group.db_subnet_group.id
  vpc_security_group_ids = [aws_security_group.this.id]
  skip_final_snapshot       = true
  final_snapshot_identifier = "document-final"
  publicly_accessible    = true
}