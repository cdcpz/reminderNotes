#Pre config
echo "Pre config"
sudo apt update
sudo apt upgrade

#Install dependencies
echo "Install dependencies"
sudo -E apt install git
sudo -E apt install curl
curl -sL https://deb.nodesource.com/setup_20.x | sudo bash -
sudo -E apt install nodejs -y
sudo -E apt install wget
sudo wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo rm packages-microsoft-prod.deb
sudo apt update
sudo apt upgrade
sudo -E apt install -y dotnet-sdk-7.0
dotnet tool install --global dotnet-ef --version 7.0
export PATH="$PATH:$HOME/.dotnet/tools/"

#Config DB
echo "Config DB"
export DEBIAN_FRONTEND=noninteractive
sudo apt install lsb-release
sudo apt install wget
sudo wget -c https://repo.mysql.com//mysql-apt-config_0.8.29-1_all.deb
sudo -E dpkg -i mysql-apt-config_0.8.29-1_all.deb
sudo apt update
sudo -E apt install -y mysql-server
sudo systemctl start mysql.service
sudo systemctl enable mysql.service
sudo service mysql stop
sudo /usr/sbin/mysqld --skip-grant-tables --skip-networking &
sudo service mysql start
#sudo mysql -u root
#sudo mysql -u root -p"root" mysql < scriptConfigDB.sql
sudo mysql -u root mysql < scriptConfigDB.sql
#USE mysql;
#UPDATE user SET plugin='mysql_native_password' WHERE User='root';
#FLUSH PRIVILEGES;
#ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'root';
#exit
sudo service mysql restart
#cd ~/ensolvers/backend/Ensolvers.Note/Ensolvers.Note.Api
cd ./backend/Ensolvers.Note/Ensolvers.Note.Api
dotnet-ef database update
dotnet run &

#Run apps
echo "Run apps"
#cd ~/ensolvers/FrontEnd/
cd ../../../FrontEnd
npm i
ng completion
ng serve -o &
#cd ~/ensolvers/backend/Ensolvers.Note/Ensolvers.Note.Api
#dotnet run &

echo "Run script Successfuly"