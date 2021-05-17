FROM node:12.2.0


EXPOSE 80
# set working directory
WORKDIR /HotelApp

# add `/app/node_modules/.bin` to $PATH
ENV PATH /HotelApp/node_modules/.bin:$PATH

# install and cache app dependencies
COPY package.json package.json
RUN npm install
RUN npm install -g @angular/cli@7.3.9

# add app
COPY . /HotelApp


# start app
CMD ["ng","serve","--configuration", "production","--host", "0.0.0.0","--port","80","--disableHostCheck"]
