cmake_minimum_required(VERSION 3.6)
project(DesignPatternDemos)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /std:c++latest /await")
#set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++0x")

# see https://cmake.org/cmake/help/v3.5/module/FindBoost.html
find_package(Boost REQUIRED)

link_directories("c:/boost/stage/lib32")
include_directories(${Boost_INCLUDE_DIR})

include_directories("c:/gtest/include")

set(SOURCE_FILES main.cpp di.hpp headers.hpp Person.cpp Person.hpp ChatRoom.cpp ChatRoom.hpp)
add_executable(DesignPatternDemos ${SOURCE_FILES})
target_link_libraries(DesignPatternDemos "c:/gtest/lib32/gtestd.lib")


file(COPY ${CMAKE_CURRENT_SOURCE_DIR}/capitals.txt
        DESTINATION ${CMAKE_CURRENT_BINARY_DIR})