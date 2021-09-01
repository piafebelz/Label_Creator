import React, { useState, useEffect } from "react";
import {
  Textarea,
  Layout,
  Header,
  HeaderLogo,
  Drawer,
  DrawerTitle,
  DrawerPlacements,
  DrawerContent,
  ActionBar,
  LayoutContent,
  Box,
  SelectHTML,
  Flex,
  FormLabel,
  FormControl,
  Input,
  Button,
} from "@oplog/express";
import "../App.css";
import Barcode from "react-hooks-barcode";

export default function BarcodeGen() {
  const [formatType, setFormatType] = useState("CODE128");
  const [colSize, setcolSize] = useState(2);
  const [sortType, setSortType] = useState("unsorted");
  const [value, setValue] = useState("");
  const [Msg, setMsg] = useState("No Barcode");
  const [searchCode, setSearchCode] = useState("");
  const [filteredArr, setFilteredArr] = useState(value.split("\n"));
  const [arr, setArr] = useState(value.split("\n"));
  const [initials, setInitials] = useState([""]);
  const [showIndex, setShowIndex] = useState(false);
  const [del, setDel] = useState(false);
  const [index, setIndex] = useState(-9999);

  useEffect(() => {
    setFilteredArr(
      value.split("\n").filter(function (i) {
        return i;
      })
    );
    setArr(
      value.split("\n").filter(function (i) {
        return i;
      })
    );
    setInitials(
      value
        .split("\n")
        .filter(function (i) {
          return i;
        })
        .map((v) => v[0])
    );
    setSearchCode("");
  }, [value]);

  useEffect(() => {
    setFilteredArr(
      value.split("\n").filter(function (i) {
        return i;
      })
    );
    updateArray();
  }, [searchCode]);

  useEffect(() => {
    switch (sortType) {
      case "unsorted":
        updateArray();
        setShowIndex(false);
        break;
      case "asc":
        setShowIndex(true);
        break;
      case "desc":
        setShowIndex(true);
        break;
      default:
        break;
    }
  }, [sortType]);

  const updateArray = () => {
    var newArray = arr.filter((word) =>
      word.toLowerCase().includes(searchCode.toLowerCase())
    );
    setFilteredArr(newArray);
  };

  const handleScroll = (id: string) => {
    const element = document.getElementById(id);
    const offset = 125;
    const bodyRect = document.body.getBoundingClientRect().top;
    const elementRect = element!.getBoundingClientRect().top;
    const elementPosition = elementRect - bodyRect;
    const offsetPosition = elementPosition - offset;
    window.scrollTo({
      top: offsetPosition,
      behavior: "smooth",
    });
  };

  const deleteBarcode = (i: number) => {
    setDel(true);
    setIndex(i);
  };

  const config = {
    // lineColor: "#ffffff",
    // background: "#4b4b4b",
    marginTop: "5px",
    marginBottom: "5px",
    fontOptions: "bold",
    width: 2,
    textMargin: 15,
    format: formatType,
    valid: (p: any) => {},
  };

  const options = [
    { value: "CODE128", label: "BARCODE-128" },
    { value: "EAN8", label: "EAN-8" },
  ];

  const sortOptions = [
    { value: "unsorted", label: "Unsorted" },
    { value: "asc", label: "Ascending" },
    { value: "desc", label: "Descending" },
  ];

  const colSizeOptions = [
    { value: "1", label: "1" },
    { value: "2", label: "2" },
    { value: "3", label: "3" },
    { value: "4", label: "4" },
    { value: "5", label: "5" },
  ];

  return (
    <div className="App">
      {console.log(colSize)}
      <Layout paddingLeft="15%">
        <Header
          style={{ backgroundColor: "#EAB543", color: "black" }}
          justifyContent="space-between"
        >
          <HeaderLogo>
            <img height="40px" src="images/icon.png" alt="logo" />
          </HeaderLogo>
          <h1 className="heading">barcode vortex</h1>
        </Header>
        <Drawer
          backgroundColor="#f0f0f0"
          width="15%"
          placement={DrawerPlacements.Left}
          isOpen
          style={{ backgroundColor: "#3d3d3d" }}
        >
          <DrawerTitle>{"Generate Barcode"}</DrawerTitle>
          <DrawerContent>
            <Flex flexDirection="column" flexWrap="wrap">
              <Box style={{ margin: "10px 10px" }}>
                <FormControl size="large">
                  <FormLabel color="white">{"Barcode Types"}</FormLabel>
                  <SelectHTML
                    onChange={(e: any) => {
                      setFormatType(e.currentTarget.value);
                      setValue("");
                      setMsg("No Barcode");
                      setSortType("unsorted");
                    }}
                    value={formatType}
                    placeholder={"Choose Format"}
                    options={options}
                    style={{
                      backgroundColor: "#4b4b4b",
                      color: "white",
                    }}
                  />
                </FormControl>
                <br />
                <FormControl size="large">
                  <FormLabel color="white">{"Barcodes / column"}</FormLabel>
                  <SelectHTML
                    onChange={(e: any) => {
                      setcolSize(e.currentTarget.value);
                    }}
                    value={colSize}
                    placeholder={"Choose Column Size"}
                    options={colSizeOptions}
                    style={{
                      backgroundColor: "#4b4b4b",
                      color: "white",
                    }}
                  />
                </FormControl>
                <br />
                <FormControl size="large">
                  <FormLabel color="white">{"Barcodes"}</FormLabel>
                  <Textarea
                    boxShadow="large"
                    height="370px"
                    placeholder="Enter Barcode(s)"
                    style={{
                      fontSize: 18,
                      borderColor: "#878787",
                      backgroundColor: "#4b4b4b",
                      color: "white",
                    }}
                    label="Enter value here"
                    value={value}
                    onChange={(e: any) => {
                      setValue(e.currentTarget.value);
                      if (!e.currentTarget.value) {
                        setMsg("No Barcode");
                      } else setMsg("");
                    }}
                    margin="normal"
                  />
                </FormControl>
              </Box>
            </Flex>
          </DrawerContent>
        </Drawer>

        <Drawer
          backgroundColor="#3d3d3d"
          color="white"
          width="12%"
          placement={DrawerPlacements.Right}
          isOpen
        >
          <DrawerTitle>{"Sort"}</DrawerTitle>

          <DrawerContent>
            <SelectHTML
              onChange={(e: any) => {
                setSortType(e.currentTarget.value);
              }}
              value={sortType}
              options={sortOptions}
              backgroundColor="#4b4b4b"
              color="white"
              margin="0 10px"
            />
          </DrawerContent>
          <DrawerTitle>{"Indexing"}</DrawerTitle>
          <span>
            <i className="far fa-info-circle fa-1x" /> Sort activates index
            search
          </span>
          <br />
          <DrawerContent className="sortPannel" height="400px">
            {Array.from(new Set(initials.sort())).map(
              (ini, indx) =>
                value !== "" && (
                  <Button
                    variant="warning"
                    disabled={!showIndex}
                    width={0.18}
                    className="indexing"
                    onClick={() => handleScroll(`_${ini}`)}
                    key={indx}
                  >
                    {ini}
                  </Button>
                )
            )}
          </DrawerContent>
        </Drawer>

        <ActionBar
          backgroundColor="#3d3d3d"
          title={"Generated Barcodes"}
        ></ActionBar>

        <LayoutContent backgroundColor="#3d3d3d">
          <Box backgroundColor="#3d3d3d">
            <Flex>
              <i
                style={{ alignSelf: "center", paddingRight: "15px" }}
                className="fa fa-search fa-2x"
              ></i>
              <Input
                placeholder={"Search Barcode"}
                type="text"
                value={searchCode}
                onChange={(e: any) => {
                  setSearchCode(e.currentTarget.value);
                  updateArray();
                }}
                backgroundColor="#4b4b4b"
                color="white"
                marginRight="15px"
              />
              <Button
                style={{
                  backgroundColor: "#EAB543",
                  color: "black",
                  fontWeight: 400,
                }}
                onClick={() => setSearchCode("")}
              >
                Clear
              </Button>
            </Flex>
            <br />

            {/* applying sorting filter on the array */}
            {sortType === "asc" && (
              <h1 style={{ display: "none" }}>{filteredArr.sort()}</h1>
            )}

            {sortType === "desc" && (
              <h1 style={{ display: "none" }}>
                {filteredArr.sort().reverse()}
              </h1>
            )}

            {/* applying delete filter on the array */}
            {del && (
              <h1 style={{ display: "none" }}>
                {filteredArr.splice(index, 1)}
                {setFilteredArr(filteredArr)}
                {setValue(filteredArr.join("\n"))}
                {setDel(false)}
                {setIndex(-9999)}
              </h1>
            )}

            <Flex flexWrap="wrap">
              {config.format === "CODE128" && arr.length !== 0 && arr[0] !== ""
                ? filteredArr.map((val, indx) => (
                    <div
                      id={`_${val[0]}`}
                      style={{
                        margin: "10px 0px",
                        padding: "0 5px",
                        width: `${100 / colSize}%`,
                        display: "flex",
                        justifyContent: "center",
                        lineHeight: "18vh",
                      }}
                      key={indx}
                    >
                      <span>
                        <strong>B{indx + 1}:</strong>{" "}
                      </span>
                      <Barcode value={val} {...config} />
                      <i
                        onClick={() => deleteBarcode(indx)}
                        className="fas fa-times-hexagon close"
                      ></i>
                    </div>
                  ))
                : null}
            </Flex>

            {config.format === "EAN8" && (
              <h3>
                Entering 7 digits will autofill BUT still type 8 digits (for
                search)
              </h3>
            )}
            <Flex flexWrap="wrap">
              {config.format === "EAN8" && arr.length !== 0 && arr[0] !== ""
                ? filteredArr.map((val, indx) =>
                    val.length === 7 || val.length === 8 ? (
                      <div
                        id={`_${val[0]}`}
                        style={{
                          margin: "10px 0px",
                          width: `${100 / colSize}%`,
                          display: "flex",
                          justifyContent: "center",
                          lineHeight: "18vh",
                        }}
                        key={indx}
                      >
                        <span>
                          <strong>B{indx + 1}:</strong>{" "}
                        </span>
                        <Barcode
                          value={val}
                          {...config}
                          {...(config.width = 2)}
                        />
                        <i
                          onClick={() => deleteBarcode(indx)}
                          className="fas fa-times-hexagon close"
                        ></i>
                      </div>
                    ) : (
                      val.length >= 8 && (
                        <div
                          style={{
                            margin: "10px 0px",
                            width: `${100 / colSize}%`,
                            display: "flex",
                            justifyContent: "center",
                            lineHeight: "18vh",
                          }}
                        >
                          Wrong Format
                          <i
                            onClick={() => deleteBarcode(indx)}
                            className="fas fa-times-hexagon close"
                            style={{ lineHeight: "0vh" }}
                          ></i>
                        </div>
                      )
                    )
                  )
                : null}
            </Flex>
            <br />
            {Msg && <h2>{Msg}</h2>}
            <br />
          </Box>
        </LayoutContent>
      </Layout>
    </div>
  );
}
